
 CREATE GLOBAL TEMPORARY TABLE "BTSTC"."PHC_TEMP_SOKHO" 
   (
	"THANG" NUMBER, 
	"NGAY_HT" DATE, 
	"NGAY_CT" DATE, 
	"SO_CHUNGTU" NVARCHAR2(50), 
	"MA_CHUNGTU" NVARCHAR2(50),
    "MA_KHO" NVARCHAR2(50),
	"LOAIHINH_DOITUONG" NVARCHAR2(20), 	
	"SODU_DAUKY" NUMBER,	
	"TON_DAU" NUMBER, 
    "SOLUONG" NUMBER, 
	"TON_CUOI" NUMBER, 
	"SODU_CUOIKY" NUMBER, 	
    "DON_GIA" NUMBER(18,2),
    "THANH_TIEN" NUMBER(18,2),
    "DIEN_GIAI" NVARCHAR2(2000),
	"USER_NAME" NVARCHAR2(50)
   ) ON COMMIT PRESERVE ROWS ;