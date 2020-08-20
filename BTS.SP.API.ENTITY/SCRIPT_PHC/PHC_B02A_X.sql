﻿--Author:HuyNQ
--Modified Date : 25/5/2018
create or replace PROCEDURE "PHC_B02A_X" (P_MA_DBHC IN VARCHAR2,P_TUNGAYHL IN DATE, P_DENNGAYHL IN DATE, cur OUT SYS_REFCURSOR) as
   QUERY_STR  VARCHAR2(32767); 
   INSERT_STR  VARCHAR2(32767); 
   CREATE_TABLE VARCHAR2(32767);
   CHECK_EXIST_TABLE NUMBER(1,0);
    IsExistCT NUMBER(18,2);
   TUNGAY_LUYKE VARCHAR2(10);
   P_NAM VARCHAR(4) := TO_CHAR(TO_DATE(P_DENNGAYHL,'DD/MM/YY'),'YYYY');
   CN_CUR SYS_REFCURSOR;
   I_MACHITIEU VARCHAR(20);
   I_THANG NUMBER(18,2);
   I_LUYKE NUMBER(18,2);
   BEGIN --START...
    TUNGAY_LUYKE := TO_CHAR ('01/01/'||TO_CHAR(P_DENNGAYHL,'YY')||'');

    --CREATE TEMP TABLE, DELETE ALL DATA FOR NEW SESSION
    CREATE_TABLE:= 'CREATE GLOBAL TEMPORARY TABLE "BTSTC"."PHC_B02AX" 
   (	
   "SAPXEP" NVARCHAR2(100), 
   "CAP" NUMBER(10,0),
   "MACHITIEU" NVARCHAR2(20), 
   "TENCHITIEU" NVARCHAR2(250),
   "DUTOANNAM" NUMBER(18,2),
   "THANG" NUMBER(18,2),
   "LUYKE" NUMBER(18,2)
   ) ON COMMIT PRESERVE ROWS';
    EXECUTE IMMEDIATE 'SELECT COUNT(*) FROM USER_TABLES WHERE TABLE_NAME = ''PHC_B02AX''' INTO CHECK_EXIST_TABLE;
    IF CHECK_EXIST_TABLE > 0 THEN DBMS_OUTPUT.PUT_LINE('ON CREATED');
    ELSE EXECUTE IMMEDIATE CREATE_TABLE;
    END IF;
    EXECUTE IMMEDIATE 'DELETE FROM PHC_B02AX';
    INSERT_STR:= 'INSERT INTO PHC_B02AX (SAPXEP, CAP, MACHITIEU, TENCHITIEU, DUTOANNAM, THANG, LUYKE) 
      SELECT DMCTIEU.SAPXEP, DMCTIEU.CAP, DMCTIEU.MACHITIEU, DMCTIEU.TENCHITIEU, SUM(NVL(DTCT.NSX_DAUNAM,0) + NVL(DTCT.NSX_BOSUNG,0) - NVL(DTCT.NSX_DIEUCHINH,0) - NVL(DTCT.NSX_HUY,0)) AS DUTOANNAM,0 AS THANG, 0 AS LUYKE 
        FROM DM_PHC_CHITIEUTHUCHI DMCTIEU
        LEFT JOIN PHC_DT_THUCHI_NDKT DT ON DMCTIEU.CODELOCATION = DT.CODELOCATION  AND DT.NAM = '||P_NAM||' AND DT.LOAICHITIEU = 1
        LEFT JOIN PHC_DT_THUCHI_NDKT_CHITIET DTCT ON DT.MADUTOAN = DTCT.MADUTOAN AND DTCT.MACHITIEU = DMCTIEU.MACHITIEU 
        WHERE DMCTIEU.CODELOCATION = '''||P_MA_DBHC||''' AND DMCTIEU.TRANG_THAI = ''A'' AND DMCTIEU.PHAN_HE = ''C'' AND DMCTIEU.LOAICHITIEU = 1
            AND (DMCTIEU.DENNGAY_HL IS NULL OR DMCTIEU.DENNGAY_HL >= TO_DATE('''||SYSDATE()||''',''DD/MM/YY'')) 
        GROUP BY DMCTIEU.SAPXEP, DMCTIEU.CAP, DMCTIEU.MACHITIEU, DMCTIEU.TENCHITIEU
        ORDER BY DMCTIEU.MACHITIEU';
    DBMS_OUTPUT.put_line(INSERT_STR); 
    EXECUTE IMMEDIATE INSERT_STR;
    --Update số liệu tháng, lũy kế 
    QUERY_STR := 'SELECT MACHITIEU FROM PHC_B02AX ORDER BY CAP DESC, MACHITIEU ASC';
    OPEN CN_CUR FOR QUERY_STR;
    LOOP
         FETCH CN_CUR INTO I_MACHITIEU;
         EXIT WHEN CN_CUR%NOTFOUND;
         I_THANG :=0;
         I_LUYKE :=0;
         SELECT COUNT(*) INTO IsExistCT FROM DM_PHC_CHITIEUTHUCHI WHERE MACHITIEU = ''||I_MACHITIEU||'' AND PHAN_HE = 'C' AND LOAICHITIEU = 1  
                AND CODELOCATION =''||P_MA_DBHC||'' AND (DENNGAY_HL IS NULL OR DENNGAY_HL >= TO_DATE(TO_CHAR(sysdate, 'MM/DD/YYYY'), 'MM/DD/YYYY')) AND CT_QT_W IS NOT NULL;
         IF IsExistCT = 1 THEN
         BEGIN
            --DBMS_OUTPUT.put_line(I_MACHITIEU||' tinh'); 
            I_THANG := NVL(PHC_FCN_TONGHOPCHITIEUTHU( ''||I_MACHITIEU||'',''||P_TUNGAYHL||'',''||P_DENNGAYHL||'',0,''||P_MA_DBHC||''),0);
            I_LUYKE := NVL(PHC_FCN_TONGHOPCHITIEUTHU( ''||I_MACHITIEU||'',''||P_TUNGAYHL||'',''||P_DENNGAYHL||'',1,''||P_MA_DBHC||''),0);
         END;
         ELSE
           -- DBMS_OUTPUT.put_line(I_MACHITIEU|| ' sum'); 
            SELECT SUM(A.THANG),SUM(A.LUYKE) INTO I_THANG, I_LUYKE FROM PHC_B02AX A
            WHERE A.MACHITIEU IN (SELECT B.MACHITIEU FROM DM_PHC_CHITIEUTHUCHI B WHERE B.MACHA = ''||I_MACHITIEU||'' AND B.PHAN_HE = 'C' AND B.LOAICHITIEU = 1  
                AND B.CODELOCATION =''||P_MA_DBHC||'' AND (B.DENNGAY_HL IS NULL OR B.DENNGAY_HL >= TO_DATE(TO_CHAR(sysdate, 'MM/DD/YYYY'), 'MM/DD/YYYY')));
            IF I_THANG IS NULL THEN I_THANG :=0; END IF;
            IF I_LUYKE IS NULL THEN I_LUYKE :=0; END IF;
        END IF;
        --DBMS_OUTPUT.put_line('tháng: '||I_THANG||' ; Lũy kế: '||I_LUYKE); 
        BEGIN
            UPDATE PHC_B02AX
             SET THANG = I_THANG,
                 LUYKE = I_LUYKE
            WHERE MACHITIEU = I_MACHITIEU;
            COMMIT;
        END;
    END LOOP;
    CLOSE CN_CUR;
   QUERY_STR:= 'SELECT * FROM PHC_B02AX ORDER BY MACHITIEU';
BEGIN 
OPEN cur FOR QUERY_STR;
EXCEPTION
   WHEN NO_DATA_FOUND
   THEN
      DBMS_OUTPUT.put_line('<your message>' || SQLERRM);
   WHEN OTHERS
   THEN
      DBMS_OUTPUT.put_line(QUERY_STR  || SQLERRM); 
END; --END RETURN VALUE

END PHC_B02A_X; --END...