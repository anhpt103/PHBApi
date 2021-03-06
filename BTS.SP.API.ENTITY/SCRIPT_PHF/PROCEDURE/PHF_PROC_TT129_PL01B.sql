create or replace PROCEDURE "PHF_PROC_TT129_PL01B" 
(
  P_TUNGAY IN DATE,
  P_DENNGAY IN DATE,
  P_TENBAOCAO IN VARCHAR,
  P_MAPHONGBAN IN VARCHAR,
  CUR OUT SYS_REFCURSOR
)
AS
QUERRY_DYNAMIC_PIVOT VARCHAR2(1000) := '';
QUERRY_SELECT_PIVOT VARCHAR2(2000) := '';
P_WHERE VARCHAR2(300) := '';
BEGIN
    IF P_MAPHONGBAN IS NULL OR P_MAPHONGBAN = '' THEN
        P_WHERE := ' 1 = 1 ';
    ELSE    
        P_WHERE := ' a.MAPHONGBAN LIKE '''||P_MAPHONGBAN||'%'' ';
    END IF;
    DECLARE
       QUERRY_CONTENT VARCHAR2(5000);
       CURSOR_CONTENT SYS_REFCURSOR;
       T_NOIDUNG VARCHAR2(300);
       T_I_STATE VARCHAR2(5);
    BEGIN
        
        QUERRY_CONTENT := 'SELECT DISTINCT(c.NOIDUNG) AS NOIDUNG,c.I_STATE FROM PHF_TT129 a 
        INNER JOIN PHF_TT129_PL01B b ON a.MA_FILE_PK = b.MA_FILE_PK 
        INNER JOIN PHF_TT129_PL01B_TEMPLATE c ON b.MADONG = c.MADONG WHERE a.TENBAOCAO = '''||P_TENBAOCAO||''' AND '||P_WHERE||'
        AND TO_DATE(a.TUNGAY,''DD-MM-YY'') >= TO_DATE('''||P_TUNGAY||''',''DD-MM-YY'') 
        AND TO_DATE(a.DENNGAY,''DD-MM-YY'') <= TO_DATE('''||P_DENNGAY||''',''DD-MM-YY'') 
        AND LENGTH(c.I_STATE) = 1 ORDER BY c.I_STATE';
         
        OPEN CURSOR_CONTENT FOR QUERRY_CONTENT;
            LOOP
               FETCH CURSOR_CONTENT INTO T_NOIDUNG,T_I_STATE;
               EXIT WHEN CURSOR_CONTENT%NOTFOUND;
               QUERRY_DYNAMIC_PIVOT := QUERRY_DYNAMIC_PIVOT || '''' || T_NOIDUNG || '''' || ' AS TIEUCHI_' || T_I_STATE || ',';
            END LOOP;
            QUERRY_DYNAMIC_PIVOT := SUBSTR(QUERRY_DYNAMIC_PIVOT,0, LENGTH(QUERRY_DYNAMIC_PIVOT) - 1);
        CLOSE CURSOR_CONTENT;
    END;
    QUERRY_SELECT_PIVOT := 'SELECT TEN,SUM(TIEUCHI_A) AS TIEUCHI_A,SUM(TIEUCHI_B) AS TIEUCHI_B,SUM(TIEUCHI_C) AS TIEUCHI_C
FROM
    (
        SELECT d.TEN,c.NOIDUNG,b.DIEM_TUDANHGIA,c.I_STATE FROM PHF_TT129 a 
        INNER JOIN PHF_TT129_PL01B b ON a.MA_FILE_PK = b.MA_FILE_PK 
        INNER JOIN PHF_TT129_PL01B_TEMPLATE c ON b.MADONG = c.MADONG 
        INNER JOIN PHF_DM_DONVI_PHONGBAN d ON d.MA = a.MAPHONGBAN 
        WHERE a.TENBAOCAO = '''||P_TENBAOCAO||''' AND '||P_WHERE||'
        AND TO_DATE(a.TUNGAY,''DD-MM-YY'') >= TO_DATE('''||P_TUNGAY||''',''DD-MM-YY'') 
        AND TO_DATE(a.DENNGAY,''DD-MM-YY'') <= TO_DATE('''||P_DENNGAY||''',''DD-MM-YY'') 
        AND LENGTH(c.I_STATE) = 1 ORDER BY c.I_STATE
    )
        PIVOT ( SUM ( DIEM_TUDANHGIA )
            FOR NOIDUNG
            IN ('||QUERRY_DYNAMIC_PIVOT||')
        ) GROUP BY TEN';
BEGIN
OPEN CUR FOR QUERRY_SELECT_PIVOT;
EXCEPTION
   WHEN NO_DATA_FOUND
   THEN
      DBMS_OUTPUT.put_line('<your message>' || SQLERRM);
   WHEN OTHERS
   THEN
      DBMS_OUTPUT.put_line('ERROR'  || SQLERRM); 
END;
END "PHF_PROC_TT129_PL01B";