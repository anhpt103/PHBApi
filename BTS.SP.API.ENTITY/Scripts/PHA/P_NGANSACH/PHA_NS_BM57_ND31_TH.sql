create or replace procedure PHA_NS_BM57_ND31_TH(P_CONGTHUC VARCHAR2, P_NAM VARCHAR2, P_CAP VARCHAR2, DONVI_TIEN number, cur OUT SYS_REFCURSOR) as
   P_INSERT_DT clob;
   P_INSERT_CHI clob;
   QUERY_STR  VARCHAR2(32767);  
   P_CT VARCHAR2(32767);   
   TEMP VARCHAR2(32767);

   BEGIN
    IF TRIM(P_CONGTHUC) IS NOT NULL THEN 
        select STC_PA_SYS.FNC_CONVERT_FORMULA_HCSN(P_CONGTHUC) INTO P_CT from dual;       
        P_CT:= ' AND ' || P_CT;
    END IF;
    
    CASE 
        WHEN P_CAP='2' THEN TEMP:=' AND A.MA_CHUONG BETWEEN ''400'' AND ''989''';
        WHEN P_CAP='3' THEN TEMP:=' AND A.MA_CHUONG BETWEEN ''600'' AND ''989''';
        WHEN P_CAP='4' THEN TEMP:=' AND A.MA_CHUONG BETWEEN ''800'' AND ''989''';
        ELSE TEMP:='';
    END CASE;
    
    EXECUTE IMMEDIATE 'TRUNCATE TABLE THDT_HCSN_DT'; 
    
    P_INSERT_DT:='INSERT INTO THDT_HCSN_DT (MA_TKTN,MA_CAP,MA_DVQHNS,MA_CHUONG,TEN_CHUONG,MA_NGANHKT,MA_LOAI,MA_CTMTQG,MA_KBNN,MA_NGUON_NSNN,NGAY_KET_SO,NGAY_HIEU_LUC,ENTERED_DR,ENTERED_CR,ACCOUNTED_DR,ACCOUNTED_CR,ATTRIBUTE8,GIA_TRI_HACH_TOAN,MA_NVC)
    SELECT A.MA_TKTN,A.MA_CAP,A.MA_DVQHNS,A.MA_CHUONG,A.TEN_CHUONG,A.MA_NGANHKT,A.MA_LOAI,A.MA_CTMTQG,A.MA_KBNN,A.MA_NGUON_NSNN,A.NGAY_KET_SO,A.NGAY_HIEU_LUC,A.ENTERED_DR,A.ENTERED_CR,A.ACCOUNTED_DR,A.ACCOUNTED_CR,A.ATTRIBUTE8,A.GIA_TRI_HACH_TOAN,A.MA_NVC from PHA_DUTOAN A
    WHERE A.NGAY_HIEU_LUC >= TO_DATE ('''|| '0101' || P_NAM  ||''', ''ddMMyyyy'')
    AND A.NGAY_HIEU_LUC <= TO_DATE ('''|| '3112' || P_NAM  ||''', ''ddMMyyyy'')
    AND NOT A.MA_CAP like ''1'' AND NOT A.MA_CHUONG LIKE ''000'' '
    || TEMP
    || P_CT || ''; 

    P_INSERT_CHI:='INSERT INTO THDT_HCSN_DT (MA_TKTN,MA_CAP,MA_DVQHNS,MA_CHUONG,TEN_CHUONG,MA_NGANHKT,MA_LOAI,MA_CTMTQG,MA_KBNN,MA_NGUON_NSNN,NGAY_KET_SO,NGAY_HIEU_LUC,ENTERED_DR,ENTERED_CR,ACCOUNTED_DR,ACCOUNTED_CR,ATTRIBUTE8,GIA_TRI_HACH_TOAN,MA_NVC)
    SELECT A.MA_TKTN,A.MA_CAP,A.MA_DVQHNS,A.MA_CHUONG,A.TEN_CHUONG,A.MA_NGANHKT,A.MA_LOAI,A.MA_CTMTQG,A.MA_KBNN,A.MA_NGUON_NSNN,A.NGAY_KET_SO,A.NGAY_HIEU_LUC,A.ENTERED_DR,A.ENTERED_CR,A.ACCOUNTED_DR,A.ACCOUNTED_CR,A.ATTRIBUTE8,A.GIA_TRI_HACH_TOAN,A.MA_NVC from PHA_HACHTOAN_CHI A
    WHERE A.NGAY_HIEU_LUC >= TO_DATE ('''|| '0101' || P_NAM  ||''', ''ddMMyyyy'')
    AND A.NGAY_HIEU_LUC <= TO_DATE ('''|| '3112' || P_NAM  ||''', ''ddMMyyyy'')
    AND NOT A.MA_CAP like ''1'' AND NOT A.MA_CHUONG LIKE ''000'' '
    || TEMP
    || P_CT || '';

    QUERY_STR:='SELECT HT.MA_CHUONG, HT.TEN_CHUONG                      
                ,NVL(HT.DTMS,0) as DTMS, NVL(HT.DTDN,0) as DTDN, NVL(HT.DTBS,0) as DTBS, NVL(HT.DTGT,0) as DTGT, NVL(HT.KPTH,0) as KPTH
                FROM
                (
                SELECT A.MA_CHUONG AS MA_CHUONG, A.TEN_CHUONG                        
                ,NVL(SUM (CASE WHEN ( ATTRIBUTE8 like ''06'' AND  MA_TKTN IN (''9523'',''9527'')) THEN A.GIA_TRI_HACH_TOAN/NVL('|| DONVI_TIEN ||',1) ELSE 0 END),0) AS DTMS                      
                ,NVL(SUM (CASE WHEN ( ATTRIBUTE8 like ''01'' AND  MA_TKTN like ''95%'') THEN A.GIA_TRI_HACH_TOAN/NVL('|| DONVI_TIEN ||',1) ELSE 0 END),0) AS DTDN     
                ,NVL(SUM (CASE WHEN ( ATTRIBUTE8 like ''02'' AND  MA_TKTN like ''95%'') THEN A.GIA_TRI_HACH_TOAN/NVL('|| DONVI_TIEN ||',1) ELSE 0 END),0) AS DTBS     
                ,NVL(SUM (CASE WHEN ( ATTRIBUTE8 like ''03'') THEN A.GIA_TRI_HACH_TOAN/NVL('|| DONVI_TIEN ||',1) ELSE 0 END),0) AS DTGT
                ,NVL(SUM (CASE WHEN ( MA_TKTN like ''81%'') THEN A.GIA_TRI_HACH_TOAN/NVL('|| DONVI_TIEN ||',1) ELSE 0 END),0) AS KPTH                
                FROM THDT_HCSN_DT A
                WHERE A.NGAY_HIEU_LUC >= TO_DATE ('''|| '0101' || P_NAM  ||''', ''ddMMyyyy'')
                AND A.NGAY_HIEU_LUC <= TO_DATE ('''|| '3112' || P_NAM  ||''', ''ddMMyyyy'')
                AND NOT A.MA_CAP like ''1'' AND NOT A.MA_CHUONG LIKE ''000'' '
                || TEMP
                || P_CT
                || ' GROUP BY A.MA_CHUONG,A.TEN_CHUONG
                ) HT
                WHERE 1=1 ORDER BY HT.MA_CHUONG';      
            
   --DBMS_OUTPUT.put_line (QUERY_STR);
   --DBMS_OUTPUT.put_line ('P_INSERT_DT:=' ||  P_INSERT_DT );
   --DBMS_OUTPUT.put_line ('P_INSERT_CHI:=' ||  P_INSERT_CHI );
   
BEGIN
    EXECUTE IMMEDIATE P_INSERT_DT;
    EXECUTE IMMEDIATE P_INSERT_CHI;
    EXECUTE IMMEDIATE QUERY_STR;
OPEN cur FOR QUERY_STR;
EXCEPTION
   WHEN NO_DATA_FOUND
   THEN
      DBMS_OUTPUT.put_line ('<your message>' || SQLERRM);
   WHEN OTHERS
   THEN
      DBMS_OUTPUT.put_line (QUERY_STR  || SQLERRM); 
END;    
END PHA_NS_BM57_ND31_TH;