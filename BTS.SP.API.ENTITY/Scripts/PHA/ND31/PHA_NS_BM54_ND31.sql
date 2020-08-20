﻿create or replace PROCEDURE PHA_NS_BM54_ND31_T (P_MA_DBHC IN NVARCHAR2,P_LOAI_BAOCAO IN VARCHAR2, P_CONGTHUC VARCHAR2, TUNGAY_KS IN DATE, DENNGAY_KS IN DATE, TUNGAY_HL IN DATE, DENNGAY_HL IN DATE, DONVI_TIEN number, cur OUT SYS_REFCURSOR) as
   QUERY_STR  CLOB; 
   INSERT_STR  CLOB; 
   INSERT_STR1  CLOB;
   QUERY_STR_CHI CLOB;
   QUERY_STR_DUTOAN CLOB; 
   P_CT VARCHAR2(32767);   
   P_SQL_INSERT VARCHAR2(32767); 
   TEMP VARCHAR2(32767);
   P_SQL_CREATE_TABLE_TH  VARCHAR2(32767);  
    N_COUNT NUMBER(17,4):=0;
    P_SELECT_DBHC VARCHAR2(32767);

   --CHI DAU TU PHAT TRIEN
   CT_CDT_QP VARCHAR2(32767);   CT_CDT_AN VARCHAR2(32767);   CT_CDT_KH VARCHAR2(32767);   CT_CDT_HDKT VARCHAR2(32767);
   CT_CDT_QLNN VARCHAR2(32767);   CT_CDT_BVMT VARCHAR2(32767);   CT_CDT_BDXH VARCHAR2(32767);   CT_CDT_VH VARCHAR2(32767);
   CT_CDT_KHCN VARCHAR2(32767);   CT_CDT_GD VARCHAR2(32767);   CT_CDT_YT VARCHAR2(32767);   CT_CDT_TDTT VARCHAR2(32767);
   CT_CDT_PT VARCHAR2(32767);  CT_CDT_HTV VARCHAR2(32767); CT_CDT_K VARCHAR2(32767);
   
    CT_DT_CDT_QP VARCHAR2(32767);   CT_DT_CDT_AN VARCHAR2(32767);   CT_DT_CDT_KH VARCHAR2(32767);   CT_DT_CDT_HDKT VARCHAR2(32767);
   CT_DT_CDT_QLNN VARCHAR2(32767);   CT_DT_CDT_BVMT VARCHAR2(32767);   CT_DT_CDT_BDXH VARCHAR2(32767);   CT_DT_CDT_VH VARCHAR2(32767);
   CT_DT_CDT_KHCN VARCHAR2(32767);   CT_DT_CDT_GD VARCHAR2(32767);   CT_DT_CDT_YT VARCHAR2(32767);   CT_DT_CDT_TDTT VARCHAR2(32767);
   CT_DT_CDT_PT VARCHAR2(32767);  CT_DT_CDT_HTV VARCHAR2(32767); CT_DT_CDT_K VARCHAR2(32767);CT_DT_CDT VARCHAR2(32767);
    
   --CHI THUONG XUYEN
   CT_CTX_QP VARCHAR2(32767);   CT_CTX_AN VARCHAR2(32767);   CT_CTX_KH VARCHAR2(32767);   CT_CTX_HDKT VARCHAR2(32767);
   CT_CTX_QLNN VARCHAR2(32767);   CT_CTX_BVMT VARCHAR2(32767);   CT_CTX_BDXH VARCHAR2(32767);   CT_CTX_VH VARCHAR2(32767);
   CT_CTX_KHCN VARCHAR2(32767);   CT_CTX_GD VARCHAR2(32767);   CT_CTX_YT VARCHAR2(32767);   CT_CTX_TDTT VARCHAR2(32767);
   CT_CTX_PT VARCHAR2(32767);
   
   
   CT_DT_CTX_QP VARCHAR2(32767);   CT_DT_CTX_AN VARCHAR2(32767);   CT_DT_CTX_KH VARCHAR2(32767);   CT_DT_CTX_HDKT VARCHAR2(32767);
   CT_DT_CTX_QLNN VARCHAR2(32767);   CT_DT_CTX_BVMT VARCHAR2(32767);   CT_DT_CTX_BDXH VARCHAR2(32767);   CT_DT_CTX_VH VARCHAR2(32767);
   CT_DT_CTX_KHCN VARCHAR2(32767);   CT_DT_CTX_GD VARCHAR2(32767);   CT_DT_CTX_YT VARCHAR2(32767);   CT_DT_CTX_TDTT VARCHAR2(32767);
   CT_DT_CTX_PT VARCHAR2(32767);CT_DT_CTX VARCHAR2(32767);
   --KHAC
   CT_CTMTCDT VARCHAR2(32767);   CT_CTMTCTX VARCHAR2(32767);   CT_CTN VARCHAR2(32767);   CT_CBS VARCHAR2(32767); CT_CCN VARCHAR2(32767);
   CT_DT_CTMTCDT VARCHAR2(32767);   CT_DT_CTMTCTX VARCHAR2(32767);
   BEGIN
    IF TRIM(P_CONGTHUC) IS NOT NULL THEN 
        select STC_PA_SYS.FNC_CONVERT_FORMULA_HCSN(P_CONGTHUC) INTO P_CT from dual;       
        P_CT:= ' AND ' || P_CT;
    END IF;

    --Gán công thức
    IF(UPPER(P_LOAI_BAOCAO) = 'DH') THEN 
        -------Chi đầu tư
         select REPLACE(CONGTHUC_DH_WHERE, '''', '''') INTO CT_CDT_QP from DM_CHITIEU_BAOCAO_COL WHERE MA_BAOCAO='CHITIEU_CHI_K_CTMTQG' AND MA_COT='CDT_QP';
         select REPLACE(CONGTHUC_DH_WHERE, '''', '''') INTO CT_CDT_AN from DM_CHITIEU_BAOCAO_COL WHERE MA_BAOCAO='CHITIEU_CHI_K_CTMTQG' AND MA_COT='CDT_AN';
         select REPLACE(CONGTHUC_DH_WHERE, '''', '''') INTO CT_CDT_KH from DM_CHITIEU_BAOCAO_COL WHERE MA_BAOCAO='CHITIEU_CHI_K_CTMTQG' AND MA_COT='CDT_KH';
         select REPLACE(CONGTHUC_DH_WHERE, '''', '''') INTO CT_CDT_HDKT from DM_CHITIEU_BAOCAO_COL WHERE MA_BAOCAO='CHITIEU_CHI_K_CTMTQG' AND MA_COT='CDT_HDKT';
         select REPLACE(CONGTHUC_DH_WHERE, '''', '''') INTO CT_CDT_QLNN from DM_CHITIEU_BAOCAO_COL WHERE MA_BAOCAO='CHITIEU_CHI_K_CTMTQG' AND MA_COT='CDT_QLNN';
         select REPLACE(CONGTHUC_DH_WHERE, '''', '''') INTO CT_CDT_BVMT from DM_CHITIEU_BAOCAO_COL WHERE MA_BAOCAO='CHITIEU_CHI_K_CTMTQG' AND MA_COT='CDT_BVMT';
         select REPLACE(CONGTHUC_DH_WHERE, '''', '''') INTO CT_CDT_BDXH from DM_CHITIEU_BAOCAO_COL WHERE MA_BAOCAO='CHITIEU_CHI_K_CTMTQG' AND MA_COT='CDT_BDXH';
         select REPLACE(CONGTHUC_DH_WHERE, '''', '''') INTO CT_CDT_VH from DM_CHITIEU_BAOCAO_COL WHERE MA_BAOCAO='CHITIEU_CHI_K_CTMTQG' AND MA_COT='CDT_VH';
         select REPLACE(CONGTHUC_DH_WHERE, '''', '''') INTO CT_CDT_KHCN from DM_CHITIEU_BAOCAO_COL WHERE MA_BAOCAO='CHITIEU_CHI_K_CTMTQG' AND MA_COT='CDT_KHCN';
         select REPLACE(CONGTHUC_DH_WHERE, '''', '''') INTO CT_CDT_GD from DM_CHITIEU_BAOCAO_COL WHERE MA_BAOCAO='CHITIEU_CHI_K_CTMTQG' AND MA_COT='CDT_GD';
         select REPLACE(CONGTHUC_DH_WHERE, '''', '''') INTO CT_CDT_YT from DM_CHITIEU_BAOCAO_COL WHERE MA_BAOCAO='CHITIEU_CHI_K_CTMTQG' AND MA_COT='CDT_YT';
         select REPLACE(CONGTHUC_DH_WHERE, '''', '''') INTO CT_CDT_TDTT from DM_CHITIEU_BAOCAO_COL WHERE MA_BAOCAO='CHITIEU_CHI_K_CTMTQG' AND MA_COT='CDT_TDTT';
         select REPLACE(CONGTHUC_DH_WHERE, '''', '''') INTO CT_CDT_PT from DM_CHITIEU_BAOCAO_COL WHERE MA_BAOCAO='CHITIEU_CHI_K_CTMTQG' AND MA_COT='CDT_PT';
         select REPLACE(CONGTHUC_DH_WHERE, '''', '''') INTO CT_CDT_HTV from DM_CHITIEU_BAOCAO_COL WHERE MA_BAOCAO='CHITIEU_CHI_K_CTMTQG' AND MA_COT='CDT_HTV';
         select REPLACE(CONGTHUC_DH_WHERE, '''', '''') INTO CT_CDT_K from DM_CHITIEU_BAOCAO_COL WHERE MA_BAOCAO='CHITIEU_CHI_K_CTMTQG' AND MA_COT='CDT_K';
         
         select REPLACE(CONGTHUC_DH_WHERE, '''', '''') INTO CT_DT_CDT_QP from DM_CHITIEU_BAOCAO_COL WHERE MA_BAOCAO='CHITIEU_CHI_K_CTMTQG' AND MA_COT='DT_CDT_QP';
         select REPLACE(CONGTHUC_DH_WHERE, '''', '''') INTO CT_DT_CDT_AN from DM_CHITIEU_BAOCAO_COL WHERE MA_BAOCAO='CHITIEU_CHI_K_CTMTQG' AND MA_COT='DT_CDT_AN';
         select REPLACE(CONGTHUC_DH_WHERE, '''', '''') INTO CT_DT_CDT_KH from DM_CHITIEU_BAOCAO_COL WHERE MA_BAOCAO='CHITIEU_CHI_K_CTMTQG' AND MA_COT='DT_CDT_KH';
         select REPLACE(CONGTHUC_DH_WHERE, '''', '''') INTO CT_DT_CDT_HDKT from DM_CHITIEU_BAOCAO_COL WHERE MA_BAOCAO='CHITIEU_CHI_K_CTMTQG' AND MA_COT='DT_CDT_HDKT';
         select REPLACE(CONGTHUC_DH_WHERE, '''', '''') INTO CT_DT_CDT_QLNN from DM_CHITIEU_BAOCAO_COL WHERE MA_BAOCAO='CHITIEU_CHI_K_CTMTQG' AND MA_COT='DT_CDT_QLNN';
         select REPLACE(CONGTHUC_DH_WHERE, '''', '''') INTO CT_DT_CDT_BVMT from DM_CHITIEU_BAOCAO_COL WHERE MA_BAOCAO='CHITIEU_CHI_K_CTMTQG' AND MA_COT='DT_CDT_BVMT';
         select REPLACE(CONGTHUC_DH_WHERE, '''', '''') INTO CT_DT_CDT_BDXH from DM_CHITIEU_BAOCAO_COL WHERE MA_BAOCAO='CHITIEU_CHI_K_CTMTQG' AND MA_COT='DT_CDT_BDXH';
         select REPLACE(CONGTHUC_DH_WHERE, '''', '''') INTO CT_DT_CDT_VH from DM_CHITIEU_BAOCAO_COL WHERE MA_BAOCAO='CHITIEU_CHI_K_CTMTQG' AND MA_COT='DT_CDT_VH';
         select REPLACE(CONGTHUC_DH_WHERE, '''', '''') INTO CT_DT_CDT_KHCN from DM_CHITIEU_BAOCAO_COL WHERE MA_BAOCAO='CHITIEU_CHI_K_CTMTQG' AND MA_COT='DT_CDT_KHCN';
         select REPLACE(CONGTHUC_DH_WHERE, '''', '''') INTO CT_DT_CDT_GD from DM_CHITIEU_BAOCAO_COL WHERE MA_BAOCAO='CHITIEU_CHI_K_CTMTQG' AND MA_COT='DT_CDT_GD';
         select REPLACE(CONGTHUC_DH_WHERE, '''', '''') INTO CT_DT_CDT_YT from DM_CHITIEU_BAOCAO_COL WHERE MA_BAOCAO='CHITIEU_CHI_K_CTMTQG' AND MA_COT='DT_CDT_YT';
         select REPLACE(CONGTHUC_DH_WHERE, '''', '''') INTO CT_DT_CDT_TDTT from DM_CHITIEU_BAOCAO_COL WHERE MA_BAOCAO='CHITIEU_CHI_K_CTMTQG' AND MA_COT='DT_CDT_TDTT';
         select REPLACE(CONGTHUC_DH_WHERE, '''', '''') INTO CT_DT_CDT_PT from DM_CHITIEU_BAOCAO_COL WHERE MA_BAOCAO='CHITIEU_CHI_K_CTMTQG' AND MA_COT='DT_CDT_PT';
         select REPLACE(CONGTHUC_DH_WHERE, '''', '''') INTO CT_DT_CDT_HTV from DM_CHITIEU_BAOCAO_COL WHERE MA_BAOCAO='CHITIEU_CHI_K_CTMTQG' AND MA_COT='DT_CDT_HTV';
         select REPLACE(CONGTHUC_DH_WHERE, '''', '''') INTO CT_DT_CDT_K from DM_CHITIEU_BAOCAO_COL WHERE MA_BAOCAO='CHITIEU_CHI_K_CTMTQG' AND MA_COT='DT_CDT_K';
         select REPLACE(CONGTHUC_DH_WHERE, '''', '''') INTO CT_DT_CDT from DM_CHITIEU_BAOCAO_COL WHERE MA_BAOCAO='CHITIEU_CHI_K_CTMTQG' AND MA_COT='DT_CDT';
         
   
        -------Chi thường xuyên
         select REPLACE(CONGTHUC_DH_WHERE, '''', '''') INTO CT_CTX_QP from DM_CHITIEU_BAOCAO_COL WHERE MA_BAOCAO='CHITIEU_CHI_K_CTMTQG' AND MA_COT='CTX_QP';
         select REPLACE(CONGTHUC_DH_WHERE, '''', '''') INTO CT_CTX_AN from DM_CHITIEU_BAOCAO_COL WHERE MA_BAOCAO='CHITIEU_CHI_K_CTMTQG' AND MA_COT='CTX_AN';
         select REPLACE(CONGTHUC_DH_WHERE, '''', '''') INTO CT_CTX_KH from DM_CHITIEU_BAOCAO_COL WHERE MA_BAOCAO='CHITIEU_CHI_K_CTMTQG' AND MA_COT='CTX_KH';
         select REPLACE(CONGTHUC_DH_WHERE, '''', '''') INTO CT_CTX_HDKT from DM_CHITIEU_BAOCAO_COL WHERE MA_BAOCAO='CHITIEU_CHI_K_CTMTQG' AND MA_COT='CTX_HDKT';
         select REPLACE(CONGTHUC_DH_WHERE, '''', '''') INTO CT_CTX_QLNN from DM_CHITIEU_BAOCAO_COL WHERE MA_BAOCAO='CHITIEU_CHI_K_CTMTQG' AND MA_COT='CTX_QLNN';
         select REPLACE(CONGTHUC_DH_WHERE, '''', '''') INTO CT_CTX_BVMT from DM_CHITIEU_BAOCAO_COL WHERE MA_BAOCAO='CHITIEU_CHI_K_CTMTQG' AND MA_COT='CTX_BVMT';
         select REPLACE(CONGTHUC_DH_WHERE, '''', '''') INTO CT_CTX_BDXH from DM_CHITIEU_BAOCAO_COL WHERE MA_BAOCAO='CHITIEU_CHI_K_CTMTQG' AND MA_COT='CTX_BDXH';
         select REPLACE(CONGTHUC_DH_WHERE, '''', '''') INTO CT_CTX_VH from DM_CHITIEU_BAOCAO_COL WHERE MA_BAOCAO='CHITIEU_CHI_K_CTMTQG' AND MA_COT='CTX_VH';
         select REPLACE(CONGTHUC_DH_WHERE, '''', '''') INTO CT_CTX_KHCN from DM_CHITIEU_BAOCAO_COL WHERE MA_BAOCAO='CHITIEU_CHI_K_CTMTQG' AND MA_COT='CTX_KHCN';
         select REPLACE(CONGTHUC_DH_WHERE, '''', '''') INTO CT_CTX_GD from DM_CHITIEU_BAOCAO_COL WHERE MA_BAOCAO='CHITIEU_CHI_K_CTMTQG' AND MA_COT='CTX_GD';
         select REPLACE(CONGTHUC_DH_WHERE, '''', '''') INTO CT_CTX_YT from DM_CHITIEU_BAOCAO_COL WHERE MA_BAOCAO='CHITIEU_CHI_K_CTMTQG' AND MA_COT='CTX_YT';
         select REPLACE(CONGTHUC_DH_WHERE, '''', '''') INTO CT_CTX_TDTT from DM_CHITIEU_BAOCAO_COL WHERE MA_BAOCAO='CHITIEU_CHI_K_CTMTQG' AND MA_COT='CTX_TDTT';
         select REPLACE(CONGTHUC_DH_WHERE, '''', '''') INTO CT_CTX_PT from DM_CHITIEU_BAOCAO_COL WHERE MA_BAOCAO='CHITIEU_CHI_K_CTMTQG' AND MA_COT='CTX_PT';
         
         select REPLACE(CONGTHUC_DH_WHERE, '''', '''') INTO CT_DT_CTX_QP from DM_CHITIEU_BAOCAO_COL WHERE MA_BAOCAO='CHITIEU_CHI_K_CTMTQG' AND MA_COT='DT_CTX_QP';
         select REPLACE(CONGTHUC_DH_WHERE, '''', '''') INTO CT_DT_CTX_AN from DM_CHITIEU_BAOCAO_COL WHERE MA_BAOCAO='CHITIEU_CHI_K_CTMTQG' AND MA_COT='DT_CTX_AN';
         select REPLACE(CONGTHUC_DH_WHERE, '''', '''') INTO CT_DT_CTX_KH from DM_CHITIEU_BAOCAO_COL WHERE MA_BAOCAO='CHITIEU_CHI_K_CTMTQG' AND MA_COT='DT_CTX_KH';
         select REPLACE(CONGTHUC_DH_WHERE, '''', '''') INTO CT_DT_CTX_HDKT from DM_CHITIEU_BAOCAO_COL WHERE MA_BAOCAO='CHITIEU_CHI_K_CTMTQG' AND MA_COT='DT_CTX_HDKT';
         select REPLACE(CONGTHUC_DH_WHERE, '''', '''') INTO CT_DT_CTX_QLNN from DM_CHITIEU_BAOCAO_COL WHERE MA_BAOCAO='CHITIEU_CHI_K_CTMTQG' AND MA_COT='DT_CTX_QLNN';
         select REPLACE(CONGTHUC_DH_WHERE, '''', '''') INTO CT_DT_CTX_BVMT from DM_CHITIEU_BAOCAO_COL WHERE MA_BAOCAO='CHITIEU_CHI_K_CTMTQG' AND MA_COT='DT_CTX_BVMT';
         select REPLACE(CONGTHUC_DH_WHERE, '''', '''') INTO CT_DT_CTX_BDXH from DM_CHITIEU_BAOCAO_COL WHERE MA_BAOCAO='CHITIEU_CHI_K_CTMTQG' AND MA_COT='DT_CTX_BDXH';
         select REPLACE(CONGTHUC_DH_WHERE, '''', '''') INTO CT_DT_CTX_VH from DM_CHITIEU_BAOCAO_COL WHERE MA_BAOCAO='CHITIEU_CHI_K_CTMTQG' AND MA_COT='DT_CTX_VH';
         select REPLACE(CONGTHUC_DH_WHERE, '''', '''') INTO CT_DT_CTX_KHCN from DM_CHITIEU_BAOCAO_COL WHERE MA_BAOCAO='CHITIEU_CHI_K_CTMTQG' AND MA_COT='DT_CTX_KHCN';
         select REPLACE(CONGTHUC_DH_WHERE, '''', '''') INTO CT_DT_CTX_GD from DM_CHITIEU_BAOCAO_COL WHERE MA_BAOCAO='CHITIEU_CHI_K_CTMTQG' AND MA_COT='DT_CTX_GD';
         select REPLACE(CONGTHUC_DH_WHERE, '''', '''') INTO CT_DT_CTX_YT from DM_CHITIEU_BAOCAO_COL WHERE MA_BAOCAO='CHITIEU_CHI_K_CTMTQG' AND MA_COT='DT_CTX_YT';
         select REPLACE(CONGTHUC_DH_WHERE, '''', '''') INTO CT_DT_CTX_TDTT from DM_CHITIEU_BAOCAO_COL WHERE MA_BAOCAO='CHITIEU_CHI_K_CTMTQG' AND MA_COT='DT_CTX_TDTT';
         select REPLACE(CONGTHUC_DH_WHERE, '''', '''') INTO CT_DT_CTX_PT from DM_CHITIEU_BAOCAO_COL WHERE MA_BAOCAO='CHITIEU_CHI_K_CTMTQG' AND MA_COT='DT_CTX_PT';
         select REPLACE(CONGTHUC_DH_WHERE, '''', '''') INTO CT_DT_CTX from DM_CHITIEU_BAOCAO_COL WHERE MA_BAOCAO='CHITIEU_CHI_K_CTMTQG' AND MA_COT='DT_CTX';
         
    END IF;
    IF(UPPER(P_LOAI_BAOCAO) = 'QT') THEN 
         -------Chi đầu tư
         select REPLACE(CONGTHUC_WHERE, '''', '''') INTO CT_CDT_QP from DM_CHITIEU_BAOCAO_COL WHERE MA_BAOCAO='CHITIEU_CHI_K_CTMTQG' AND MA_COT='CDT_QP';
         select REPLACE(CONGTHUC_WHERE, '''', '''') INTO CT_CDT_AN from DM_CHITIEU_BAOCAO_COL WHERE MA_BAOCAO='CHITIEU_CHI_K_CTMTQG' AND MA_COT='CDT_AN';
         select REPLACE(CONGTHUC_WHERE, '''', '''') INTO CT_CDT_KH from DM_CHITIEU_BAOCAO_COL WHERE MA_BAOCAO='CHITIEU_CHI_K_CTMTQG' AND MA_COT='CDT_KH';
         select REPLACE(CONGTHUC_WHERE, '''', '''') INTO CT_CDT_HDKT from DM_CHITIEU_BAOCAO_COL WHERE MA_BAOCAO='CHITIEU_CHI_K_CTMTQG' AND MA_COT='CDT_HDKT';
         select REPLACE(CONGTHUC_WHERE, '''', '''') INTO CT_CDT_QLNN from DM_CHITIEU_BAOCAO_COL WHERE MA_BAOCAO='CHITIEU_CHI_K_CTMTQG' AND MA_COT='CDT_QLNN';
         select REPLACE(CONGTHUC_WHERE, '''', '''') INTO CT_CDT_BVMT from DM_CHITIEU_BAOCAO_COL WHERE MA_BAOCAO='CHITIEU_CHI_K_CTMTQG' AND MA_COT='CDT_BVMT';
         select REPLACE(CONGTHUC_WHERE, '''', '''') INTO CT_CDT_BDXH from DM_CHITIEU_BAOCAO_COL WHERE MA_BAOCAO='CHITIEU_CHI_K_CTMTQG' AND MA_COT='CDT_BDXH';
         select REPLACE(CONGTHUC_WHERE, '''', '''') INTO CT_CDT_VH from DM_CHITIEU_BAOCAO_COL WHERE MA_BAOCAO='CHITIEU_CHI_K_CTMTQG' AND MA_COT='CDT_VH';
         select REPLACE(CONGTHUC_WHERE, '''', '''') INTO CT_CDT_KHCN from DM_CHITIEU_BAOCAO_COL WHERE MA_BAOCAO='CHITIEU_CHI_K_CTMTQG' AND MA_COT='CDT_KHCN';
         select REPLACE(CONGTHUC_WHERE, '''', '''') INTO CT_CDT_GD from DM_CHITIEU_BAOCAO_COL WHERE MA_BAOCAO='CHITIEU_CHI_K_CTMTQG' AND MA_COT='CDT_GD';
         select REPLACE(CONGTHUC_WHERE, '''', '''') INTO CT_CDT_YT from DM_CHITIEU_BAOCAO_COL WHERE MA_BAOCAO='CHITIEU_CHI_K_CTMTQG' AND MA_COT='CDT_YT';
         select REPLACE(CONGTHUC_WHERE, '''', '''') INTO CT_CDT_TDTT from DM_CHITIEU_BAOCAO_COL WHERE MA_BAOCAO='CHITIEU_CHI_K_CTMTQG' AND MA_COT='CDT_TDTT';
         select REPLACE(CONGTHUC_WHERE, '''', '''') INTO CT_CDT_PT from DM_CHITIEU_BAOCAO_COL WHERE MA_BAOCAO='CHITIEU_CHI_K_CTMTQG' AND MA_COT='CDT_PT';
         select REPLACE(CONGTHUC_WHERE, '''', '''') INTO CT_CDT_HTV from DM_CHITIEU_BAOCAO_COL WHERE MA_BAOCAO='CHITIEU_CHI_K_CTMTQG' AND MA_COT='CDT_HTV';
         select REPLACE(CONGTHUC_WHERE, '''', '''') INTO CT_CDT_K from DM_CHITIEU_BAOCAO_COL WHERE MA_BAOCAO='CHITIEU_CHI_K_CTMTQG' AND MA_COT='CDT_K';
        -------Chi thường xuyên
         select REPLACE(CONGTHUC_WHERE, '''', '''') INTO CT_CTX_QP from DM_CHITIEU_BAOCAO_COL WHERE MA_BAOCAO='CHITIEU_CHI_K_CTMTQG' AND MA_COT='CTX_QP';
         select REPLACE(CONGTHUC_WHERE, '''', '''') INTO CT_CTX_AN from DM_CHITIEU_BAOCAO_COL WHERE MA_BAOCAO='CHITIEU_CHI_K_CTMTQG' AND MA_COT='CTX_AN';
         select REPLACE(CONGTHUC_WHERE, '''', '''') INTO CT_CTX_KH from DM_CHITIEU_BAOCAO_COL WHERE MA_BAOCAO='CHITIEU_CHI_K_CTMTQG' AND MA_COT='CTX_KH';
         select REPLACE(CONGTHUC_WHERE, '''', '''') INTO CT_CTX_HDKT from DM_CHITIEU_BAOCAO_COL WHERE MA_BAOCAO='CHITIEU_CHI_K_CTMTQG' AND MA_COT='CTX_HDKT';
         select REPLACE(CONGTHUC_WHERE, '''', '''') INTO CT_CTX_QLNN from DM_CHITIEU_BAOCAO_COL WHERE MA_BAOCAO='CHITIEU_CHI_K_CTMTQG' AND MA_COT='CTX_QLNN';
         select REPLACE(CONGTHUC_WHERE, '''', '''') INTO CT_CTX_BVMT from DM_CHITIEU_BAOCAO_COL WHERE MA_BAOCAO='CHITIEU_CHI_K_CTMTQG' AND MA_COT='CTX_BVMT';
         select REPLACE(CONGTHUC_WHERE, '''', '''') INTO CT_CTX_BDXH from DM_CHITIEU_BAOCAO_COL WHERE MA_BAOCAO='CHITIEU_CHI_K_CTMTQG' AND MA_COT='CTX_BDXH';
         select REPLACE(CONGTHUC_WHERE, '''', '''') INTO CT_CTX_VH from DM_CHITIEU_BAOCAO_COL WHERE MA_BAOCAO='CHITIEU_CHI_K_CTMTQG' AND MA_COT='CTX_VH';
         select REPLACE(CONGTHUC_WHERE, '''', '''') INTO CT_CTX_KHCN from DM_CHITIEU_BAOCAO_COL WHERE MA_BAOCAO='CHITIEU_CHI_K_CTMTQG' AND MA_COT='CTX_KHCN';
         select REPLACE(CONGTHUC_WHERE, '''', '''') INTO CT_CTX_GD from DM_CHITIEU_BAOCAO_COL WHERE MA_BAOCAO='CHITIEU_CHI_K_CTMTQG' AND MA_COT='CTX_GD';
         select REPLACE(CONGTHUC_WHERE, '''', '''') INTO CT_CTX_YT from DM_CHITIEU_BAOCAO_COL WHERE MA_BAOCAO='CHITIEU_CHI_K_CTMTQG' AND MA_COT='CTX_YT';
         select REPLACE(CONGTHUC_WHERE, '''', '''') INTO CT_CTX_TDTT from DM_CHITIEU_BAOCAO_COL WHERE MA_BAOCAO='CHITIEU_CHI_K_CTMTQG' AND MA_COT='CTX_TDTT';
         select REPLACE(CONGTHUC_WHERE, '''', '''') INTO CT_CTX_PT from DM_CHITIEU_BAOCAO_COL WHERE MA_BAOCAO='CHITIEU_CHI_K_CTMTQG' AND MA_COT='CTX_PT';        
    END IF;
    -------KHAC
     select REPLACE(CONGTHUC_WHERE, '''', '''') INTO CT_CTMTCDT from DM_CHITIEU_BAOCAO_COL WHERE MA_BAOCAO='BM54_ND31' AND MA_COT='CTMTCDT';
     select REPLACE(CONGTHUC_WHERE, '''', '''') INTO CT_CTMTCTX from DM_CHITIEU_BAOCAO_COL WHERE MA_BAOCAO='BM54_ND31' AND MA_COT='CTMTCTX';
     select REPLACE(CONGTHUC_WHERE, '''', '''') INTO CT_DT_CTMTCDT from DM_CHITIEU_BAOCAO_COL WHERE MA_BAOCAO='BM54_ND31' AND MA_COT='DTCTMTCDT';
     select REPLACE(CONGTHUC_WHERE, '''', '''') INTO CT_DT_CTMTCTX from DM_CHITIEU_BAOCAO_COL WHERE MA_BAOCAO='BM54_ND31' AND MA_COT='DTCTMTCTX';
     select REPLACE(CONGTHUC_WHERE, '''', '''') INTO CT_CTN from DM_CHITIEU_BAOCAO_COL WHERE MA_BAOCAO='BM54_ND31' AND MA_COT='CTN';
     select REPLACE(CONGTHUC_WHERE, '''', '''') INTO CT_CBS from DM_CHITIEU_BAOCAO_COL WHERE MA_BAOCAO='BM54_ND31' AND MA_COT='CBS';
     select REPLACE(CONGTHUC_WHERE, '''', '''') INTO CT_CCN from DM_CHITIEU_BAOCAO_COL WHERE MA_BAOCAO='BM54_ND31' AND MA_COT='CCN';
    
     P_SQL_CREATE_TABLE_TH := 'CREATE TABLE BM54_ND31(
        LOAI_CHITIEU NUMBER,MA_TKTN NVARCHAR2(100), MA_DIABAN NVARCHAR2(100), MA_CAP NVARCHAR2(100),MA_CAPMLNS NVARCHAR2(100),MA_CHUONG NVARCHAR2(100),TEN_CHUONG NVARCHAR2(2000),MA_NGANHKT NVARCHAR2(100),MA_LOAI NVARCHAR2(100),
        MA_TIEUMUC NVARCHAR2(100), MA_MUC NVARCHAR2(100), MA_TIEUNHOM NVARCHAR2(100),MA_NHOM NVARCHAR2(100),MA_CTMTQG NVARCHAR2(100),MA_KBNN NVARCHAR2(100), NGAY_HIEU_LUC DATE, NGAY_KET_SO DATE,LOAI_DU_TOAN NVARCHAR2(100),
        MA_NGUON_NSNN NVARCHAR2(100),MA_DVQHNS NVARCHAR2(100), MA_NVC NVARCHAR2(100), GIA_TRI_HACH_TOAN NUMBER)SEGMENT CREATION IMMEDIATE 
  PCTFREE 10 PCTUSED 40 INITRANS 1 MAXTRANS 255 NOCOMPRESS LOGGING
  STORAGE(INITIAL 65536 NEXT 1048576 MINEXTENTS 1 MAXEXTENTS 2147483645
  PCTINCREASE 0 FREELISTS 1 FREELIST GROUPS 1 BUFFER_POOL DEFAULT FLASH_CACHE DEFAULT CELL_FLASH_CACHE DEFAULT)
  ';         

    QUERY_STR_CHI := 'SELECT 2,MA_TKTN ,MA_DIABAN,MA_CAP,MA_CAPMLNS,MA_CHUONG,TEN_CHUONG,MA_NGANHKT,MA_LOAI,MA_TIEUMUC,MA_MUC,MA_TIEUNHOM,MA_NHOM,MA_CTMTQG,MA_KBNN,MA_NGUON_NSNN,MA_DVQHNS,MA_NVC,TO_DATE(to_char(NGAY_HIEU_LUC,''ddMMyyyy''), ''ddMMyyyy''),TO_DATE (to_char(NGAY_KET_SO,''ddMMyyyy''), ''ddMMyyyy''),ATTRIBUTE8
        ,SUM (GIA_TRI_HACH_TOAN) FROM PHA_HACHTOAN_CHI VC 
         GROUP BY  MA_TKTN,MA_DIABAN,MA_CAP,MA_CAPMLNS,MA_CHUONG,TEN_CHUONG,MA_NGANHKT,MA_LOAI,MA_TIEUMUC,MA_MUC,MA_TIEUNHOM,MA_NHOM,MA_CTMTQG,MA_KBNN,MA_NGUON_NSNN,MA_DVQHNS,MA_NVC,NGAY_HIEU_LUC,NGAY_KET_SO,ATTRIBUTE8';   
        INSERT_STR := 'INSERT INTO BM54_ND31(LOAI_CHITIEU,MA_TKTN ,MA_DIABAN,MA_CAP,MA_CAPMLNS,MA_CHUONG,TEN_CHUONG,MA_NGANHKT,MA_LOAI, MA_TIEUMUC,MA_MUC,MA_TIEUNHOM,MA_NHOM,MA_CTMTQG,MA_KBNN,MA_NGUON_NSNN,MA_DVQHNS,MA_NVC,NGAY_HIEU_LUC,NGAY_KET_SO,LOAI_DU_TOAN,GIA_TRI_HACH_TOAN)('||QUERY_STR_CHI||')';
  
   QUERY_STR_DUTOAN := 'SELECT 3,MA_TKTN ,MA_DIABAN,MA_CAP,MA_CAPMLNS,MA_CHUONG,TEN_CHUONG,MA_NGANHKT,MA_LOAI,MA_TIEUMUC,MA_MUC,MA_TIEUNHOM,MA_NHOM,MA_CTMTQG,MA_KBNN,MA_NGUON_NSNN,MA_DVQHNS,MA_NVC,TO_DATE(to_char(NGAY_HIEU_LUC,''ddMMyyyy''), ''ddMMyyyy''),TO_DATE (to_char(NGAY_KET_SO,''ddMMyyyy''), ''ddMMyyyy''),ATTRIBUTE8
        ,SUM (GIA_TRI_HACH_TOAN) FROM PHA_DUTOAN VC 
         GROUP BY  MA_TKTN,MA_DIABAN,MA_CAP,MA_CAPMLNS,MA_CHUONG,TEN_CHUONG,MA_NGANHKT,MA_LOAI,MA_TIEUMUC,MA_MUC,MA_TIEUNHOM,MA_NHOM,MA_CTMTQG,MA_KBNN,MA_NGUON_NSNN,MA_DVQHNS,MA_NVC,NGAY_HIEU_LUC,NGAY_KET_SO,ATTRIBUTE8';   
        INSERT_STR1 := 'INSERT INTO BM54_ND31(LOAI_CHITIEU,MA_TKTN ,MA_DIABAN,MA_CAP,MA_CAPMLNS,MA_CHUONG,TEN_CHUONG,MA_NGANHKT,MA_LOAI, MA_TIEUMUC,MA_MUC,MA_TIEUNHOM,MA_NHOM,MA_CTMTQG,MA_KBNN,MA_NGUON_NSNN,MA_DVQHNS,MA_NVC,NGAY_HIEU_LUC,NGAY_KET_SO,LOAI_DU_TOAN,GIA_TRI_HACH_TOAN)('||QUERY_STR_DUTOAN||')';
  BEGIN
        SELECT COUNT(*)  INTO N_COUNT  FROM ALL_TAB_COLUMNS  WHERE TABLE_NAME = 'BM54_ND31';
        EXCEPTION WHEN OTHERS THEN N_COUNT:=0;
    END;
  IF(N_COUNT IS NULL OR N_COUNT<=0) THEN
    BEGIN
        EXECUTE IMMEDIATE P_SQL_CREATE_TABLE_TH;  
        EXECUTE IMMEDIATE INSERT_STR; 
        EXECUTE IMMEDIATE INSERT_STR1;    
    END;
    END IF;
--    IF(N_COUNT > 0) THEN
--    BEGIN
--        EXECUTE IMMEDIATE 'TRUNCATE TABLE BM54_ND31';
--        EXECUTE IMMEDIATE INSERT_STR; 
--        EXECUTE IMMEDIATE INSERT_STR1;    
--    END;
--    END IF;
    IF(P_MA_DBHC <> 27) THEN
        P_SELECT_DBHC:= ' AND MA_DIABAN IN (SELECT MA_DBHC FROM DM_DBHC WHERE MA_DBHC IN (SELECT MA_DBHC FROM DM_DBHC WHERE MA_DBHC = '''||P_MA_DBHC||''' OR MA_DBHC_CHA = '''||P_MA_DBHC||''') OR MA_DBHC_CHA IN (SELECT MA_DBHC FROM DM_DBHC WHERE MA_DBHC = '''||P_MA_DBHC||''' OR MA_DBHC_CHA = '''||P_MA_DBHC||'''))';
        ELSE P_SELECT_DBHC:= ' ';
        END IF;
        IF(P_SELECT_DBHC IS NOT NULL) THEN
        P_CT:=P_CT || P_SELECT_DBHC;
        ELSE
        P_CT:=P_CT;
        END IF;
    QUERY_STR:='SELECT HT.MA_CHUONG, HT.TEN_CHUONG, HT.MA_DVQHNS, NS.TEN_DVQHNS, HT.MA_KBNN, HT.MA_DIABAN, DB.TEN_DBHC
                , NVL(HT.CDT_QP,0) as CDT_QP, NVL(HT.CDT_AN,0) as CDT_AN, NVL(HT.CDT_KH,0) as CDT_KH, NVL(HT.CDT_HDKT,0) as CDT_HDKT, NVL(HT.CDT_QLNN,0) as CDT_QLNN
                , NVL(HT.CDT_BVMT,0) as CDT_BVMT, NVL(HT.CDT_BDXH,0) as CDT_BDXH, NVL(HT.CDT_VH,0) as CDT_VH, NVL(HT.CDT_KHCN,0) as CDT_KHCN
                , NVL(HT.CDT_GD,0) as CDT_GD, NVL(HT.CDT_YT,0) as CDT_YT, NVL(HT.CDT_TDTT,0) as CDT_TDTT, NVL(HT.CDT_PT,0) as CDT_PT , NVL(HT.CDT_HTV,0) as CDT_HTV, NVL(HT.CDT_K,0) as CDT_K  
                
                , NVL(HT.DT_CDT_QP,0) as DT_CDT_QP, NVL(HT.DT_CDT_AN,0) as DT_CDT_AN, NVL(HT.DT_CDT_KH,0) as DT_CDT_KH, NVL(HT.DT_CDT_HDKT,0) as DT_CDT_HDKT, NVL(HT.DT_CDT_QLNN,0) as DT_CDT_QLNN
                , NVL(HT.DT_CDT_BVMT,0) as DT_CDT_BVMT, NVL(HT.DT_CDT_BDXH,0) as DT_CDT_BDXH, NVL(HT.DT_CDT_VH,0) as DT_CDT_VH, NVL(HT.DT_CDT_KHCN,0) as DT_CDT_KHCN
                , NVL(HT.DT_CDT_GD,0) as DT_CDT_GD, NVL(HT.DT_CDT_YT,0) as DT_CDT_YT, NVL(HT.DT_CDT_TDTT,0) as DT_CDT_TDTT, NVL(HT.DT_CDT_PT,0) as DT_CDT_PT , NVL(HT.DT_CDT_HTV,0) as DT_CDT_HTV, NVL(HT.DT_CDT_K,0) as DT_CDT_K
                , NVL(HT.DT_CDT,0) as DT_CDT
                , NVL(HT.CTX_QP,0) as  CTX_QP, NVL(HT.CTX_AN,0) as  CTX_AN, NVL(HT.CTX_KH,0) as CTX_KH, NVL(HT.CTX_HDKT,0) as CTX_HDKT, NVL(HT.CTX_QLNN,0) as CTX_QLNN
                , NVL(HT.CTX_BVMT,0) as CTX_BVMT, NVL(HT.CTX_BDXH,0) as CTX_BDXH, NVL(HT.CTX_VH,0) as CTX_VH, NVL(HT.CTX_KHCN,0) as CTX_KHCN
                , NVL(HT.CTX_GD,0) as CTX_GD, NVL(HT.CTX_YT,0) as CTX_YT, NVL(HT.CTX_TDTT,0) as CTX_TDTT, NVL(HT.CTX_PT,0) as CTX_PT
                
                , NVL(HT.DT_CTX_QP,0) as  DT_CTX_QP, NVL(HT.DT_CTX_AN,0) as  DT_CTX_AN, NVL(HT.DT_CTX_KH,0) as DT_CTX_KH, NVL(HT.DT_CTX_HDKT,0) as DT_CTX_HDKT, NVL(HT.DT_CTX_QLNN,0) as DT_CTX_QLNN
                , NVL(HT.DT_CTX_BVMT,0) as DT_CTX_BVMT, NVL(HT.DT_CTX_BDXH,0) as DT_CTX_BDXH, NVL(HT.DT_CTX_VH,0) as DT_CTX_VH, NVL(HT.DT_CTX_KHCN,0) as DT_CTX_KHCN
                , NVL(HT.DT_CTX_GD,0) as DT_CTX_GD, NVL(HT.DT_CTX_YT,0) as DT_CTX_YT, NVL(HT.DT_CTX_TDTT,0) as DT_CTX_TDTT, NVL(HT.DT_CTX_PT,0) as DT_CTX_PT
                , NVL(HT.DT_CTX,0) as DT_CTX
                , NVL(HT.CTMTCDT,0) as CTMTCDT, NVL(HT.CTMTCTX,0) as CTMTCTX, NVL(HT.DT_CTMTCDT,0) as DT_CTMTCDT, NVL(HT.DT_CTMTCTX,0) as DT_CTMTCTX, NVL(HT.CTN,0) as CTN, NVL(HT.CBS,0) as CBS, NVL(HT.CCN,0) as CCN                      
                FROM
                (
                SELECT A.MA_CHUONG AS MA_CHUONG, A.TEN_CHUONG, A.MA_DVQHNS, A.MA_KBNN, A.MA_DIABAN   
                        ,NVL(SUM (CASE WHEN ('|| CT_CDT_QP ||')     THEN A.GIA_TRI_HACH_TOAN/NVL('|| DONVI_TIEN ||',1) ELSE 0 END),0) AS CDT_QP
                        ,NVL(SUM (CASE WHEN ('|| CT_CDT_AN ||')     THEN A.GIA_TRI_HACH_TOAN/NVL('|| DONVI_TIEN ||',1) ELSE 0 END),0) AS CDT_AN
                        ,NVL(SUM (CASE WHEN ('|| CT_CDT_KH ||')     THEN A.GIA_TRI_HACH_TOAN/NVL('|| DONVI_TIEN ||',1) ELSE 0 END),0) AS CDT_KH
                        ,NVL(SUM (CASE WHEN ('|| CT_CDT_HDKT ||')   THEN A.GIA_TRI_HACH_TOAN/NVL('|| DONVI_TIEN ||',1) ELSE 0 END),0) AS CDT_HDKT
                        ,NVL(SUM (CASE WHEN ('|| CT_CDT_QLNN ||')   THEN A.GIA_TRI_HACH_TOAN/NVL('|| DONVI_TIEN ||',1) ELSE 0 END),0) AS CDT_QLNN
                        ,NVL(SUM (CASE WHEN ('|| CT_CDT_BVMT ||')   THEN A.GIA_TRI_HACH_TOAN/NVL('|| DONVI_TIEN ||',1) ELSE 0 END),0) AS CDT_BVMT
                        ,NVL(SUM (CASE WHEN ('|| CT_CDT_BDXH ||')   THEN A.GIA_TRI_HACH_TOAN/NVL('|| DONVI_TIEN ||',1) ELSE 0 END),0) AS CDT_BDXH
                        ,NVL(SUM (CASE WHEN ('|| CT_CDT_VH ||')     THEN A.GIA_TRI_HACH_TOAN/NVL('|| DONVI_TIEN ||',1) ELSE 0 END),0) AS CDT_VH
                        ,NVL(SUM (CASE WHEN ('|| CT_CDT_KHCN ||')   THEN A.GIA_TRI_HACH_TOAN/NVL('|| DONVI_TIEN ||',1) ELSE 0 END),0) AS CDT_KHCN
                        ,NVL(SUM (CASE WHEN ('|| CT_CDT_GD ||')     THEN A.GIA_TRI_HACH_TOAN/NVL('|| DONVI_TIEN ||',1) ELSE 0 END),0) AS CDT_GD
                        ,NVL(SUM (CASE WHEN ('|| CT_CDT_YT ||')     THEN A.GIA_TRI_HACH_TOAN/NVL('|| DONVI_TIEN ||',1) ELSE 0 END),0) AS CDT_YT
                        ,NVL(SUM (CASE WHEN ('|| CT_CDT_TDTT ||')   THEN A.GIA_TRI_HACH_TOAN/NVL('|| DONVI_TIEN ||',1) ELSE 0 END),0) AS CDT_TDTT
                        ,NVL(SUM (CASE WHEN ('|| CT_CDT_PT ||')     THEN A.GIA_TRI_HACH_TOAN/NVL('|| DONVI_TIEN ||',1) ELSE 0 END),0) AS CDT_PT
                        ,NVL(SUM (CASE WHEN ('|| CT_CDT_HTV ||')     THEN A.GIA_TRI_HACH_TOAN/NVL('|| DONVI_TIEN ||',1) ELSE 0 END),0) AS CDT_HTV
                        ,NVL(SUM (CASE WHEN ('|| CT_CDT_K ||')     THEN A.GIA_TRI_HACH_TOAN/NVL('|| DONVI_TIEN ||',1) ELSE 0 END),0) AS CDT_K
                        
                        ,NVL(SUM (CASE WHEN ('|| CT_DT_CDT_QP ||')     THEN A.GIA_TRI_HACH_TOAN/NVL('|| DONVI_TIEN ||',1) ELSE 0 END),0) AS DT_CDT_QP
                         ,NVL(SUM (CASE WHEN ('|| CT_DT_CDT_AN ||')     THEN A.GIA_TRI_HACH_TOAN/NVL('|| DONVI_TIEN ||',1) ELSE 0 END),0) AS DT_CDT_AN
                        ,NVL(SUM (CASE WHEN ('|| CT_DT_CDT_KH ||')     THEN A.GIA_TRI_HACH_TOAN/NVL('|| DONVI_TIEN ||',1) ELSE 0 END),0) AS DT_CDT_KH
                        ,NVL(SUM (CASE WHEN ('|| CT_DT_CDT_HDKT ||')   THEN A.GIA_TRI_HACH_TOAN/NVL('|| DONVI_TIEN ||',1) ELSE 0 END),0) AS DT_CDT_HDKT
                        ,NVL(SUM (CASE WHEN ('|| CT_DT_CDT_QLNN ||')   THEN A.GIA_TRI_HACH_TOAN/NVL('|| DONVI_TIEN ||',1) ELSE 0 END),0) AS DT_CDT_QLNN
                        ,NVL(SUM (CASE WHEN ('|| CT_DT_CDT_BVMT ||')   THEN A.GIA_TRI_HACH_TOAN/NVL('|| DONVI_TIEN ||',1) ELSE 0 END),0) AS DT_CDT_BVMT
                        ,NVL(SUM (CASE WHEN ('|| CT_DT_CDT_BDXH ||')   THEN A.GIA_TRI_HACH_TOAN/NVL('|| DONVI_TIEN ||',1) ELSE 0 END),0) AS DT_CDT_BDXH
                        ,NVL(SUM (CASE WHEN ('|| CT_DT_CDT_VH ||')     THEN A.GIA_TRI_HACH_TOAN/NVL('|| DONVI_TIEN ||',1) ELSE 0 END),0) AS DT_CDT_VH
                        ,NVL(SUM (CASE WHEN ('|| CT_DT_CDT_KHCN ||')   THEN A.GIA_TRI_HACH_TOAN/NVL('|| DONVI_TIEN ||',1) ELSE 0 END),0) AS DT_CDT_KHCN
                        ,NVL(SUM (CASE WHEN ('|| CT_DT_CDT_GD ||')     THEN A.GIA_TRI_HACH_TOAN/NVL('|| DONVI_TIEN ||',1) ELSE 0 END),0) AS DT_CDT_GD
                        ,NVL(SUM (CASE WHEN ('|| CT_DT_CDT_YT ||')     THEN A.GIA_TRI_HACH_TOAN/NVL('|| DONVI_TIEN ||',1) ELSE 0 END),0) AS DT_CDT_YT
                        ,NVL(SUM (CASE WHEN ('|| CT_DT_CDT_TDTT ||')   THEN A.GIA_TRI_HACH_TOAN/NVL('|| DONVI_TIEN ||',1) ELSE 0 END),0) AS DT_CDT_TDTT
                        ,NVL(SUM (CASE WHEN ('|| CT_DT_CDT_PT ||')     THEN A.GIA_TRI_HACH_TOAN/NVL('|| DONVI_TIEN ||',1) ELSE 0 END),0) AS DT_CDT_PT
                        ,NVL(SUM (CASE WHEN ('|| CT_DT_CDT_HTV ||')     THEN A.GIA_TRI_HACH_TOAN/NVL('|| DONVI_TIEN ||',1) ELSE 0 END),0) AS DT_CDT_HTV
                        ,NVL(SUM (CASE WHEN ('|| CT_DT_CDT_K ||')     THEN A.GIA_TRI_HACH_TOAN/NVL('|| DONVI_TIEN ||',1) ELSE 0 END),0) AS DT_CDT_K
                        ,NVL(SUM (CASE WHEN ('|| CT_DT_CDT ||')     THEN A.GIA_TRI_HACH_TOAN/NVL('|| DONVI_TIEN ||',1) ELSE 0 END),0) AS DT_CDT

                        ,NVL(SUM (CASE WHEN ('|| CT_CTX_QP ||')     THEN A.GIA_TRI_HACH_TOAN/NVL('|| DONVI_TIEN ||',1) ELSE 0 END),0) AS CTX_QP
                        ,NVL(SUM (CASE WHEN ('|| CT_CTX_AN ||')     THEN A.GIA_TRI_HACH_TOAN/NVL('|| DONVI_TIEN ||',1) ELSE 0 END),0) AS CTX_AN
                        ,NVL(SUM (CASE WHEN ('|| CT_CTX_KH ||')     THEN A.GIA_TRI_HACH_TOAN/NVL('|| DONVI_TIEN ||',1) ELSE 0 END),0) AS CTX_KH
                        ,NVL(SUM (CASE WHEN ('|| CT_CTX_HDKT ||')   THEN A.GIA_TRI_HACH_TOAN/NVL('|| DONVI_TIEN ||',1) ELSE 0 END),0) AS CTX_HDKT
                        ,NVL(SUM (CASE WHEN ('|| CT_CTX_QLNN ||')   THEN A.GIA_TRI_HACH_TOAN/NVL('|| DONVI_TIEN ||',1) ELSE 0 END),0) AS CTX_QLNN
                        ,NVL(SUM (CASE WHEN ('|| CT_CTX_BVMT ||')   THEN A.GIA_TRI_HACH_TOAN/NVL('|| DONVI_TIEN ||',1) ELSE 0 END),0) AS CTX_BVMT
                        ,NVL(SUM (CASE WHEN ('|| CT_CTX_BDXH ||')   THEN A.GIA_TRI_HACH_TOAN/NVL('|| DONVI_TIEN ||',1) ELSE 0 END),0) AS CTX_BDXH
                        ,NVL(SUM (CASE WHEN ('|| CT_CTX_VH ||')     THEN A.GIA_TRI_HACH_TOAN/NVL('|| DONVI_TIEN ||',1) ELSE 0 END),0) AS CTX_VH
                        ,NVL(SUM (CASE WHEN ('|| CT_CTX_KHCN ||')   THEN A.GIA_TRI_HACH_TOAN/NVL('|| DONVI_TIEN ||',1) ELSE 0 END),0) AS CTX_KHCN
                        ,NVL(SUM (CASE WHEN ('|| CT_CTX_GD ||')     THEN A.GIA_TRI_HACH_TOAN/NVL('|| DONVI_TIEN ||',1) ELSE 0 END),0) AS CTX_GD
                        ,NVL(SUM (CASE WHEN ('|| CT_CTX_YT ||')     THEN A.GIA_TRI_HACH_TOAN/NVL('|| DONVI_TIEN ||',1) ELSE 0 END),0) AS CTX_YT
                        ,NVL(SUM (CASE WHEN ('|| CT_CTX_TDTT ||')   THEN A.GIA_TRI_HACH_TOAN/NVL('|| DONVI_TIEN ||',1) ELSE 0 END),0) AS CTX_TDTT
                        ,NVL(SUM (CASE WHEN ('|| CT_CTX_PT ||')     THEN A.GIA_TRI_HACH_TOAN/NVL('|| DONVI_TIEN ||',1) ELSE 0 END),0) AS CTX_PT
                        
                        ,NVL(SUM (CASE WHEN ('|| CT_DT_CTX_QP ||')     THEN A.GIA_TRI_HACH_TOAN/NVL('|| DONVI_TIEN ||',1) ELSE 0 END),0) AS DT_CTX_QP
                        ,NVL(SUM (CASE WHEN ('|| CT_DT_CTX_AN ||')     THEN A.GIA_TRI_HACH_TOAN/NVL('|| DONVI_TIEN ||',1) ELSE 0 END),0) AS DT_CTX_AN
                        ,NVL(SUM (CASE WHEN ('|| CT_DT_CTX_KH ||')     THEN A.GIA_TRI_HACH_TOAN/NVL('|| DONVI_TIEN ||',1) ELSE 0 END),0) AS DT_CTX_KH
                        ,NVL(SUM (CASE WHEN ('|| CT_DT_CTX_HDKT ||')   THEN A.GIA_TRI_HACH_TOAN/NVL('|| DONVI_TIEN ||',1) ELSE 0 END),0) AS DT_CTX_HDKT
                        ,NVL(SUM (CASE WHEN ('|| CT_DT_CTX_QLNN ||')   THEN A.GIA_TRI_HACH_TOAN/NVL('|| DONVI_TIEN ||',1) ELSE 0 END),0) AS DT_CTX_QLNN
                        ,NVL(SUM (CASE WHEN ('|| CT_DT_CTX_BVMT ||')   THEN A.GIA_TRI_HACH_TOAN/NVL('|| DONVI_TIEN ||',1) ELSE 0 END),0) AS DT_CTX_BVMT
                        ,NVL(SUM (CASE WHEN ('|| CT_DT_CTX_BDXH ||')   THEN A.GIA_TRI_HACH_TOAN/NVL('|| DONVI_TIEN ||',1) ELSE 0 END),0) AS DT_CTX_BDXH
                        ,NVL(SUM (CASE WHEN ('|| CT_DT_CTX_VH ||')     THEN A.GIA_TRI_HACH_TOAN/NVL('|| DONVI_TIEN ||',1) ELSE 0 END),0) AS DT_CTX_VH
                        ,NVL(SUM (CASE WHEN ('|| CT_DT_CTX_KHCN ||')   THEN A.GIA_TRI_HACH_TOAN/NVL('|| DONVI_TIEN ||',1) ELSE 0 END),0) AS DT_CTX_KHCN
                        ,NVL(SUM (CASE WHEN ('|| CT_DT_CTX_GD ||')     THEN A.GIA_TRI_HACH_TOAN/NVL('|| DONVI_TIEN ||',1) ELSE 0 END),0) AS DT_CTX_GD
                        ,NVL(SUM (CASE WHEN ('|| CT_DT_CTX_YT ||')     THEN A.GIA_TRI_HACH_TOAN/NVL('|| DONVI_TIEN ||',1) ELSE 0 END),0) AS DT_CTX_YT
                        ,NVL(SUM (CASE WHEN ('|| CT_DT_CTX_TDTT ||')   THEN A.GIA_TRI_HACH_TOAN/NVL('|| DONVI_TIEN ||',1) ELSE 0 END),0) AS DT_CTX_TDTT
                        ,NVL(SUM (CASE WHEN ('|| CT_DT_CTX_PT ||')     THEN A.GIA_TRI_HACH_TOAN/NVL('|| DONVI_TIEN ||',1) ELSE 0 END),0) AS DT_CTX_PT
                        ,NVL(SUM (CASE WHEN ('|| CT_DT_CTX ||')     THEN A.GIA_TRI_HACH_TOAN/NVL('|| DONVI_TIEN ||',1) ELSE 0 END),0) AS DT_CTX

                        ,NVL(SUM (CASE WHEN ('|| CT_CTMTCDT ||')    THEN A.GIA_TRI_HACH_TOAN/NVL('|| DONVI_TIEN ||',1) ELSE 0 END),0) AS CTMTCDT
                        ,NVL(SUM (CASE WHEN ('|| CT_CTMTCTX ||')    THEN A.GIA_TRI_HACH_TOAN/NVL('|| DONVI_TIEN ||',1) ELSE 0 END),0) AS CTMTCTX
                        ,NVL(SUM (CASE WHEN ('|| CT_DT_CTMTCDT ||')    THEN A.GIA_TRI_HACH_TOAN/NVL('|| DONVI_TIEN ||',1) ELSE 0 END),0) AS DT_CTMTCDT
                        ,NVL(SUM (CASE WHEN ('|| CT_DT_CTMTCTX ||')    THEN A.GIA_TRI_HACH_TOAN/NVL('|| DONVI_TIEN ||',1) ELSE 0 END),0) AS DT_CTMTCTX
                        ,NVL(SUM (CASE WHEN ('|| CT_CTN ||')        THEN A.GIA_TRI_HACH_TOAN/NVL('|| DONVI_TIEN ||',1) ELSE 0 END),0) AS CTN
                        ,NVL(SUM (CASE WHEN ('|| CT_CBS ||')        THEN A.GIA_TRI_HACH_TOAN/NVL('|| DONVI_TIEN ||',1) ELSE 0 END),0) AS CBS
                        ,NVL(SUM (CASE WHEN ('|| CT_CCN ||')        THEN A.GIA_TRI_HACH_TOAN/NVL('|| DONVI_TIEN ||',1) ELSE 0 END),0) AS CCN    
                FROM BM54_ND31 A
                WHERE A.NGAY_HIEU_LUC >= TO_DATE ('''||to_char(TUNGAY_HL,'ddMMyyyy')||''', ''ddMMyyyy'')
                AND A.NGAY_HIEU_LUC <= TO_DATE ('''||to_char(DENNGAY_HL,'ddMMyyyy')||''', ''ddMMyyyy'')     
                and A.NGAY_KET_SO <=TO_DATE ('''||to_char(DENNGAY_KS,'ddMMyyyy')||''', ''ddMMyyyy'')
                and A.NGAY_KET_SO >=TO_DATE ('''||to_char(TUNGAY_KS,'ddMMyyyy')||''', ''ddMMyyyy'')                         
                    AND NOT A.MA_CAP like ''1'' AND NOT A.MA_CHUONG LIKE ''000'' AND A.MA_CAPMLNS = ''2'''
                    || P_CT
                || ' GROUP BY A.MA_CHUONG,A.TEN_CHUONG, A.MA_DVQHNS, A.MA_KBNN, A.MA_DIABAN
                ) HT LEFT JOIN SYS_DVQHNS NS ON HT.MA_DVQHNS = NS.MA_DVQHNS
                LEFT JOIN DM_DBHC DB ON HT.MA_DIABAN = DB.MA_DBHC  
                WHERE 1=1 AND (HT.CDT_QP <> 0 OR HT.CDT_AN <> 0 OR HT.CDT_KH <> 0 OR HT.CDT_HDKT <> 0 OR HT.CDT_QLNN<> 0 OR HT.CDT_BVMT <> 0
                OR HT.CDT_BDXH <> 0 OR HT.CDT_VH <> 0 OR HT.CDT_KHCN <> 0 OR HT.CDT_GD <>0 OR HT.CDT_YT <> 0 OR HT.CDT_TDTT <> 0 OR HT.CDT_PT <> 0 OR HT.CDT_HTV <>0 OR HT.CDT_K <> 0
                OR HT.DT_CDT_QP <> 0 OR HT.DT_CDT_QP <> 0 OR HT.DT_CDT_AN <> 0 OR HT.DT_CDT_KH <> 0 OR HT.DT_CDT_HDKT <> 0 OR HT.DT_CDT_QLNN<> 0 OR HT.DT_CDT_BVMT <> 0
                OR HT.DT_CDT_BDXH <> 0 OR HT.DT_CDT_VH <> 0 OR HT.DT_CDT_KHCN <> 0 OR HT.DT_CDT_GD <>0 OR HT.DT_CDT_YT <> 0 OR HT.DT_CDT_TDTT <> 0 OR HT.DT_CDT_PT <> 0 OR HT.DT_CDT_HTV <>0 OR HT.DT_CDT_K <> 0
                OR HT.DT_CDT <> 0 OR HT.CTX_QP <> 0 OR HT.CTX_AN <> 0 OR HT.CTX_KH <> 0 OR HT.CTX_HDKT <> 0 OR HT.CTX_QLNN<> 0 OR HT.CTX_BVMT <> 0
                OR HT.CTX_BDXH <> 0 OR HT.CTX_VH <> 0 OR HT.CTX_KHCN <> 0 OR HT.CTX_GD <>0 OR HT.CTX_YT <> 0 OR HT.CTX_TDTT <> 0 OR HT.CTX_PT <> 0 OR HT.DT_CTX_QP <>0 OR HT.DT_CTX_AN <> 0
                OR HT.DT_CTX_KH <> 0 OR HT.DT_CTX_HDKT <> 0 OR HT.DT_CTX_QLNN <> 0 OR HT.DT_CTX_BVMT <> 0 OR HT.DT_CTX_BDXH <> 0 OR HT.DT_CTX_VH <> 0 OR HT.DT_CTX_KHCN <> 0
                OR HT.DT_CTX_GD <> 0 OR HT.DT_CTX_YT <> 0 OR HT.DT_CTX_TDTT <> 0 OR HT.DT_CTX_PT <>0 OR HT.DT_CTX <> 0 OR HT.CTMTCDT <> 0 OR HT.CTMTCTX <> 0 OR HT.DT_CTMTCDT <>0 OR HT.DT_CTMTCTX <> 0
                OR HT.CTN <> 0 OR HT.CBS <> 0 OR HT.CCN <> 0) ORDER BY HT.MA_KBNN';

BEGIN
EXECUTE IMMEDIATE QUERY_STR;
OPEN cur FOR QUERY_STR;
EXCEPTION
   WHEN NO_DATA_FOUND
   THEN
      DBMS_OUTPUT.put_line ('<your message>' || SQLERRM);
   WHEN OTHERS
   THEN
   DBMS_OUTPUT.ENABLE (buffer_size => NULL);
      --DBMS_OUTPUT.put_line (QUERY_STR  || SQLERRM); 
      DBMS_OUTPUT.put_line ('<your message>' || SQLERRM);
END;   
END PHA_NS_BM54_ND31_T;