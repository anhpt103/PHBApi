create or replace FUNCTION "PHC_FCN_TONGHOPCHITIEUCHI" ( P_MACHITIEU IN NVARCHAR2, P_TUNGAY IN DATE, P_DENNGAY IN DATE, P_ISLUYKE IN NUMBER, P_MA_DBHC IN NVARCHAR2) 
RETURN NUMBER IS
    P_WHERE VARCHAR2(32767);
    v_TOTALNUMBER NUMBER;
    IsExistCT NUMBER;
    QUERY_STR VARCHAR2(32767);
    RESULT_NUMBER NUMBER;
    CURSOR_RESULT SYS_REFCURSOR;
    TUNGAY_LUYKE VARCHAR2(10);
BEGIN
    DBMS_OUTPUT.ENABLE(1000000);
    TUNGAY_LUYKE := TO_CHAR ('01/01/'||TO_CHAR(P_TUNGAY,'YY')||'');
    RESULT_NUMBER := 0;
    SELECT COUNT(*) INTO IsExistCT FROM DM_PHC_CHITIEUTHUCHI WHERE MACHITIEU = ''||P_MACHITIEU||'' AND PHAN_HE = 'C' AND LOAICHITIEU = 2 AND CODELOCATION =''||P_MA_DBHC||'' AND (DENNGAY_HL IS NULL OR DENNGAY_HL >= TO_DATE(TO_CHAR(sysdate, 'MM/DD/YYYY'), 'MM/DD/YYYY'));
    IF IsExistCT = 1 THEN
        SELECT CT_QT_W INTO P_WHERE FROM DM_PHC_CHITIEUTHUCHI WHERE MACHITIEU = ''||P_MACHITIEU||'' AND PHAN_HE = 'C' AND LOAICHITIEU = 2 AND CODELOCATION =''||P_MA_DBHC||''  AND (DENNGAY_HL IS NULL OR DENNGAY_HL >= TO_DATE(TO_CHAR(sysdate, 'MM/DD/YYYY'), 'MM/DD/YYYY'));
    ELSE
        P_WHERE:= '';
    END IF;
    IF P_WHERE IS NULL OR P_WHERE = '' THEN
        P_WHERE := ' AND 1 = 1 ';
    ELSE
        P_WHERE := ' AND ' || P_WHERE;
    END IF;
    --N?: +, C� -
    --ISLUYKE =0:T�nh t?ng th�ng, ISLUYKE: T�nh theo l?y k?
    IF P_ISLUYKE = 0 THEN
    QUERY_STR :='SELECT SUM(NVL(SOTIEN,0)) 
                FROM V_PHC_CHUNGTU
                WHERE LOAI_TAIKHOAN = ''N'' AND NGAY_HT >= TO_DATE('''||P_TUNGAY||''',''DD/MM/YY'') AND NGAY_HT <= TO_DATE('''||P_DENNGAY||''',''DD/MM/YY'') AND CODELOCATION ='''||P_MA_DBHC||''' '||P_WHERE||''
                     ; 
    ELSIF P_ISLUYKE = 1 THEN
        QUERY_STR :='SELECT SUM(NVL(SOTIEN,0))
                FROM V_PHC_CHUNGTU
                WHERE LOAI_TAIKHOAN = ''N'' AND NGAY_HT >= TO_DATE('''||TUNGAY_LUYKE||''',''DD/MM/YY'') AND NGAY_HT <= TO_DATE('''||P_DENNGAY||''',''DD/MM/YY'') AND CODELOCATION ='''||P_MA_DBHC||''' '||P_WHERE||''
                        ; 
    ELSIF P_ISLUYKE = 2 THEN
        QUERY_STR :='SELECT SUM(NVL(TIEN_NSNN,0))
                FROM V_PHC_CHUNGTU
                WHERE LOAI_TAIKHOAN = ''N'' AND NGAY_HT >= TO_DATE('''||P_TUNGAY||''',''DD/MM/YY'') AND NGAY_HT <= TO_DATE('''||P_DENNGAY||''',''DD/MM/YY'') AND CODELOCATION ='''||P_MA_DBHC||''' '||P_WHERE||''
                        ; 
    END IF;

    DECLARE
        T_NUMBER NUMBER;
    BEGIN
    --DBMS_OUTPUT.put_line('QUERY_STR'  || QUERY_STR); 
    OPEN CURSOR_RESULT FOR QUERY_STR;
    --------
      LOOP
          FETCH CURSOR_RESULT INTO T_NUMBER;
          EXIT WHEN CURSOR_RESULT%NOTFOUND; 
          RESULT_NUMBER := T_NUMBER;
       END LOOP;

    END;
    CLOSE CURSOR_RESULT;
    RETURN RESULT_NUMBER;
END PHC_FCN_TONGHOPCHITIEUCHI;