
  CREATE OR REPLACE PACKAGE "BTSTC"."STC_PA_SYS" 
AS
   FUNCTION FNC_GET_TEN_DM (TAB_NAME   IN VARCHAR,
                            F_NAME        VARCHAR,
                            F_CODE        VARCHAR,
                            S_VALUE    IN VARCHAR,
                            EFF_DATE      DATE)
      RETURN NVARCHAR2;

   FUNCTION FNC_GET_DICTIONARY (F_NAME      VARCHAR,
                                F_CODE      VARCHAR,
                                EFF_DATE    DATE)
      RETURN NVARCHAR2;

   FUNCTION FNC_GET_CONVERT_CHUONG2CAP (F_CHUONG NVARCHAR2)
      RETURN NVARCHAR2;


   FUNCTION FNC_GET_CONVERT_NKT2LOAI (F_NKT NVARCHAR2)
      RETURN NVARCHAR2;

   FUNCTION FNC_GET_CONVERT_NDKT2MUC (F_NDKT NVARCHAR2)
      RETURN NVARCHAR2;

   FUNCTION FNC_GET_LOAI_NS (F_MUC NVARCHAR2)
      RETURN NVARCHAR2;

   FUNCTION FNC_CONVERT_FORMULA (F_FORMULA NVARCHAR2)
      RETURN NVARCHAR2;

   FUNCTION FNC_GET_TEN_CHITIEU (p_Ma_CHITIEU IN VARCHAR2, p_NGAY_HL IN DATE)
      RETURN VARCHAR2;

   FUNCTION FNC_GET_CONGTHUC_CHITIEU (p_Ma_CHITIEU   IN VARCHAR2,
                                      p_NGAY_HL      IN DATE)
      RETURN VARCHAR2;

   FUNCTION FNC_GET_CONVERT_NDKT2NHOM (F_NDKT NVARCHAR2)
      RETURN NVARCHAR2;

   FUNCTION FNC_GET_CONVERT_NDKT2TNHOM (F_NDKT NVARCHAR2)
      RETURN NVARCHAR2;
-- phongnt add procedure TH_MLNS
-- Ngay 01-07-2017----------------------------------
     PROCEDURE PRC_TH_MLNS_SYS (P_BNGAY_HACHTOAN    DATE,
                          P_ENGAY_HACHTOAN    DATE,
                          P_BNGAY_KETSO       DATE,
                          P_ENGAY_KETSO       DATE,
                          P_LOAI              VARCHAR2,
                          P_CONGTHUC          VARCHAR2);
----------------------------------------------------
    
   PROCEDURE PRC_TH_MLNS (P_BNGAY_HACHTOAN    DATE,
                          P_ENGAY_HACHTOAN    DATE,
                          P_BNGAY_KETSO       DATE,
                          P_ENGAY_KETSO       DATE,
                          P_LOAI              VARCHAR2);

   PROCEDURE PRC_TH_MLNS_EXCELL (P_BNGAY_HACHTOAN    DATE,
                                 P_ENGAY_HACHTOAN    DATE,
                                 P_BNGAY_KETSO       DATE,
                                 P_ENGAY_KETSO       DATE,
                                 P_LOAI              VARCHAR2,
                                 P_USERID            VARCHAR2);

   PROCEDURE PRC_TH_TONQUY (P_BNGAY_HACHTOAN DATE, P_ENGAY_HACHTOAN DATE);

   PROCEDURE PRC_SUM_UP;

   PROCEDURE PRC_GET_DATA (F_LOAIDATA      NUMBER,
                           F_TABLE_NAME    NCHAR,
                           F_TUNGAY        DATE,
                           F_DENNGAY       DATE,
                           F_SHKB          NCHAR);

   PROCEDURE PRC_TH_MLNS_BYFOMULAR (P_BNGAY_HACHTOAN    DATE,
                                    P_ENGAY_HACHTOAN    DATE,
                                    P_BNGAY_KETSO       DATE,
                                    P_ENGAY_KETSO       DATE,
                                    P_LOAI              NVARCHAR2,
                                    P_USERID            NVARCHAR2,
                                    P_CONGTHUC          NVARCHAR2);
END STC_PA_SYS;

/