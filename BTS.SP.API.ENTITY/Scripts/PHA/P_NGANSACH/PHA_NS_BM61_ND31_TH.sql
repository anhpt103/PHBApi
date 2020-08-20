create or replace procedure PHA_NS_BM61_ND31_TH(P_CONGTHUC VARCHAR2, P_NAM VARCHAR2, MaCapThap VARCHAR2, DONVI_TIEN number, cur OUT SYS_REFCURSOR) as
   QUERY_STR  CLOB; 
   P_CT VARCHAR2(32767);   
   P_SQL_INSERT VARCHAR2(32767);     
    TEMP VARCHAR2(32767);
     P_CAP VARCHAR2(32767);

   BEGIN
    IF TRIM(P_CONGTHUC) IS NOT NULL THEN 
        select STC_PA_SYS.FNC_CONVERT_FORMULA_HCSN(P_CONGTHUC) INTO P_CT from dual;
        --P_SQL_INSERT:= ' '||P_SQL_INSERT ||' and '||P_CT;
        P_CT:= ' AND ' || P_CT; 
        P_CAP:= MaCapThap;

    END IF;
            CASE 
                WHEN P_CAP='2' THEN TEMP:=' AND A.MA_CHUONG BETWEEN ''400'' AND ''799''';
                WHEN P_CAP='3' THEN TEMP:=' AND A.MA_CHUONG BETWEEN ''600'' AND ''989''';
                WHEN P_CAP='4' THEN TEMP:=' AND A.MA_CHUONG BETWEEN ''800'' AND ''989''';
                ELSE TEMP:='';
            END CASE;

            QUERY_STR:='SELECT (case when HT.MA_CHUONG>=400 AND HT.MA_CHUONG<=599 then Cast(''CAP_TINH'' as nvarchar2(50)) when HT.MA_CHUONG>=600 AND HT.MA_CHUONG<=799 then  Cast(''CAP_HUYEN'' as nvarchar2(50)) 
                                when HT.MA_CHUONG>=800 AND HT.MA_CHUONG<=989 then  Cast(''CAP_XA'' as nvarchar2(50)) else Cast(''CAP_KHAC'' as nvarchar2(50)) end) as MA_NGANSACH
                        ,HT.MA_CHUONG, HT.TEN_CHUONG
                        , NVL(HT.TS,0) as TS, NVL(HT.DTPT,0) as DTPT, NVL(HT.KPSN,0) as KPSN, NVL(HT.TSBS,0) as TSBS, NVL(HT.TSCDT,0) as TSCDT, NVL(HT.CDTVTN,0) as CDTVTN
                        , NVL(HT.CDTNVN,0) as CDTNVN, NVL(HT.TSKP,0) as TSKP, NVL(HT.KPVTN,0) as KPVTN, NVL(HT.KPVNN,0) as KPVNN
                        , NVL(HT.CTX_KH,0) as CTX_35020 , NVL(HT.CTX_HDKT,0) as CTX_34700 , NVL(HT.CTX_QLNN,0) as CTX_34800
                        , NVL(HT.CTX_DB,0) as CTX_35000 , NVL(HT.CTX_TG,0) as CTX_35010 , NVL(HT.CTX_BVMT,0) as CTX_34600, NVL(HT.CTX_BDXH,0) as CTX_34900, NVL(HT.CTX_AN,0) as CTX_34030, NVL(HT.CTX_VH,0) as CTX_34300
                        , NVL(HT.CTX_KHCN,0) as CTX_34080, NVL(HT.CTX_QP,0) as CTX_34020, NVL(HT.CTX_GD,0) as CTX_34040, NVL(HT.CTX_YT,0) as CTX_34090, NVL(HT.CTX_TDTT,0) as CTX_34500, NVL(HT.CTX_PT,0) as CTX_34400
                        FROM
                        (
                        SELECT A.MA_CHUONG AS MA_CHUONG, A.TEN_CHUONG
                        ,NVL(SUM (CASE WHEN ( NOT A.MA_CTMTQG like ''00000'') THEN A.GIA_TRI_HACH_TOAN/NVL('|| DONVI_TIEN ||',1) ELSE 0 END),0) AS TS
                        ,NVL(SUM (CASE WHEN ((((MA_TKTN like ''1713'' OR  MA_TKTN like ''1716'' OR  MA_TKTN like ''1916'' OR  MA_TKTN like ''1919'' OR  MA_TKTN like ''8211'' OR  MA_TKTN like ''8221'' OR  MA_TKTN like ''8916'' OR  MA_TKTN like ''8919'' OR  MA_TKTN like ''8956'' OR  MA_TKTN like ''8957''  OR  MA_TKTN like ''8958'' OR  MA_TKTN like ''8959'') AND ( MA_NGANHKT like ''468'') AND ( MA_MUC  BETWEEN  ''9100'' AND ''9400'')) OR (( MA_TKTN like ''1713'' OR  MA_TKTN like ''1716'' OR  MA_TKTN like ''1916'' OR  MA_TKTN like ''1919'' OR  MA_TKTN like ''8211'' OR  MA_TKTN like ''8221'' OR  MA_TKTN like ''8916'' OR  MA_TKTN like ''8919'' OR  MA_TKTN like ''8956'' OR  MA_TKTN like ''8957''  OR  MA_TKTN like ''8958'' OR  MA_TKTN like ''8959'') AND  ( MA_NGANHKT like ''471'') AND ( MA_MUC  BETWEEN  ''9100'' AND ''9400''))OR (( MA_TKTN like ''1713'' OR  MA_TKTN like ''1716'' OR  MA_TKTN like ''1916'' OR  MA_TKTN like ''1919'' OR  MA_TKTN like ''8211'' OR  MA_TKTN like ''8221'' OR  MA_TKTN like ''8916'' OR  MA_TKTN like ''8919'' OR  MA_TKTN like ''8956'' OR  MA_TKTN like ''8957''  OR  MA_TKTN like ''8958'' OR  MA_TKTN like ''8959'') AND ( MA_LOAI like ''490'') AND ( MA_MUC  BETWEEN  ''9100'' AND ''9400''))OR (( MA_TKTN like ''1713'' OR  MA_TKTN like ''1716'' OR  MA_TKTN like ''1916'' OR  MA_TKTN like ''1919'' OR  MA_TKTN like ''8211'' OR  MA_TKTN like ''8221'' OR  MA_TKTN like ''8916'' OR  MA_TKTN like ''8919'' OR  MA_TKTN like ''8956'' OR  MA_TKTN like ''8957''  OR  MA_TKTN like ''8958'' OR  MA_TKTN like ''8959'') AND ( MA_LOAI like ''370'') AND ( MA_MUC  BETWEEN  ''9100'' AND ''9400''))OR (( MA_TKTN like ''1713'' OR  MA_TKTN like ''1716'' OR  MA_TKTN like ''1916'' OR  MA_TKTN like ''1919'' OR  MA_TKTN like ''8211'' OR  MA_TKTN like ''8221'' OR  MA_TKTN like ''8916'' OR  MA_TKTN like ''8919'' OR  MA_TKTN like ''8956'' OR  MA_TKTN like ''8957''  OR  MA_TKTN like ''8958'' OR  MA_TKTN like ''8959'') AND ( MA_NGANHKT like ''521'' OR  MA_NGANHKT like ''522'' OR  MA_NGANHKT like ''523'' OR  MA_NGANHKT like ''526'' OR  MA_NGANHKT like ''532'' OR  MA_NGANHKT like ''533'') AND ( MA_MUC  BETWEEN  ''9100'' AND ''9400'')) OR (( MA_TKTN like ''1713'' OR  MA_TKTN like ''1716'' OR  MA_TKTN like ''8211'' OR  MA_TKTN like ''8221'' OR  MA_TKTN like ''8956'' OR  MA_TKTN like ''8957'' OR  MA_TKTN like ''8958'' OR  MA_TKTN like ''8959'') AND ( MA_LOAI like ''550'' AND NOT ( MA_NGANHKT like ''558''  OR  MA_NGANHKT like ''561'' OR  MA_NGANHKT like ''562'')) AND ( MA_MUC  BETWEEN  ''9100'' AND ''9400'')) OR (( MA_TKTN like ''1713'' OR  MA_TKTN like ''1716'' OR  MA_TKTN like ''1916'' OR  MA_TKTN like ''1919'' OR  MA_TKTN like ''8211'' OR  MA_TKTN like ''8221'' OR  MA_TKTN like ''8916'' OR  MA_TKTN like ''8919'' OR  MA_TKTN like ''8956'' OR  MA_TKTN like ''8957''  OR  MA_TKTN like ''8958'' OR  MA_TKTN like ''8959'') AND ( MA_NGANHKT like ''252'' OR  MA_NGANHKT like ''253'' OR  MA_NGANHKT like ''254'') AND ( MA_MUC  BETWEEN  ''9100'' AND ''9400'')) OR (( MA_TKTN like ''1713'' OR  MA_TKTN like ''1716'' OR  MA_TKTN like ''1916'' OR  MA_TKTN like ''1919'' OR  MA_TKTN like ''8211'' OR  MA_TKTN like ''8221'' OR  MA_TKTN like ''8916'' OR  MA_TKTN like ''8919'' OR  MA_TKTN like ''8956'' OR  MA_TKTN like ''8957''  OR  MA_TKTN like ''8958'' OR  MA_TKTN like ''8959'') AND ( MA_NGANHKT like ''562'') AND ( MA_MUC  BETWEEN  ''9100'' AND ''9400''))OR (( MA_TKTN like ''1713'' OR  MA_TKTN like ''1716'' OR  MA_TKTN like ''1916'' OR  MA_TKTN like ''1919'' OR  MA_TKTN like ''8211'' OR  MA_TKTN like ''8221'' OR  MA_TKTN like ''8916'' OR  MA_TKTN like ''8919'' OR  MA_TKTN like ''8956'' OR  MA_TKTN like ''8957''  OR  MA_TKTN like ''8958'' OR  MA_TKTN like ''8959'') AND ( MA_LOAI like ''280'') AND ( MA_MUC  BETWEEN  ''9100'' AND ''9400''))OR ((( MA_TKTN like ''1713'' OR  MA_TKTN like ''1716'' OR  MA_TKTN like ''1916'' OR  MA_TKTN like ''1919'' OR  MA_TKTN like ''8211'' OR  MA_TKTN like ''8221'' OR  MA_TKTN like ''8916'' OR  MA_TKTN like ''8919'' OR  MA_TKTN like ''8956'' OR  MA_TKTN like ''8957''  OR  MA_TKTN like ''8958'' OR  MA_TKTN like ''8959'') AND ( MA_LOAI like ''220'') AND ( MA_MUC  BETWEEN  ''9100'' AND ''9400''))OR (( MA_TKTN like ''1713'' OR  MA_TKTN like ''1716'' OR  MA_TKTN like ''8211'' OR  MA_TKTN like ''8221'' OR  MA_TKTN  BETWEEN  ''8956'' AND ''8959'')  AND   MA_MUC  BETWEEN  ''9100'' AND ''9400''  AND   MA_LOAI like ''010'')OR(( MA_TKTN like ''1713'' OR  MA_TKTN like ''1716''  OR  MA_TKTN like ''1916'' OR  MA_TKTN like ''1919'' OR  MA_TKTN like ''8211'' OR  MA_TKTN like ''8221'' OR  MA_TKTN like ''8916'' OR  MA_TKTN like ''8919'' OR  MA_TKTN like ''8956'' OR  MA_TKTN like ''8957'' OR  MA_TKTN like ''8958'' OR  MA_TKTN like ''8959'') AND (( MA_LOAI like ''040'' OR  MA_LOAI like ''070'' OR  MA_LOAI like ''130'' OR  MA_LOAI like ''160'' OR  MA_LOAI like ''190'' OR  MA_LOAI like ''250'' OR  MA_LOAI like ''310'' OR  MA_LOAI like ''400'' OR  MA_LOAI like ''430'')  AND NOT  MA_NGANHKT  BETWEEN  ''252'' AND ''254'') OR ( MA_NGANHKT like ''558'' OR  MA_NGANHKT like ''561'' OR  MA_NGANHKT like ''582'' OR  MA_NGANHKT like ''583'') AND ( MA_MUC  BETWEEN  ''9100'' AND ''9400''))) OR ((( MA_TKTN like ''1713'' OR  MA_TKTN like ''1716'' OR  MA_TKTN like ''8211'' OR  MA_TKTN like ''8221'' OR  MA_TKTN  BETWEEN  ''8956'' AND ''8959'')  AND   MA_MUC  BETWEEN  ''9100'' AND ''9400''  AND ( MA_NGANHKT  BETWEEN  ''461'' AND ''467'' OR   MA_NGANHKT like ''472'' OR  MA_NGANHKT like ''473'' OR  MA_NGANHKT like ''489''))) OR (( MA_TKTN like ''1713'' OR  MA_TKTN like ''1716'' OR  MA_TKTN like ''1916'' OR  MA_TKTN like ''1919'' OR  MA_TKTN like ''8211'' OR  MA_TKTN like ''8221'' OR  MA_TKTN like ''8916'' OR  MA_TKTN like ''8919'' OR  MA_TKTN like ''8956'' OR  MA_TKTN like ''8957'' OR  MA_TKTN like ''8958'' OR  MA_TKTN like ''8959'') AND ( MA_NGANHKT like ''474'' OR  MA_NGANHKT like ''524'' OR  MA_NGANHKT like ''525'' OR  MA_NGANHKT like ''527'' OR  MA_NGANHKT like ''528'' OR  MA_NGANHKT like ''531'') AND ( MA_MUC  BETWEEN  ''9100'' AND ''9400''))OR ((( MA_TKTN like ''1713'' OR  MA_TKTN like ''1716'' OR  MA_TKTN like ''8211'' OR  MA_TKTN like ''8221'' OR  MA_TKTN  BETWEEN  ''8956'' AND ''8959'')  AND   MA_MUC  BETWEEN  ''9100'' AND ''9400'' AND (( MA_LOAI like ''340'' OR  MA_LOAI like ''610'' OR  MA_LOAI like ''640'') OR ( MA_NGANHKT like ''581'')))))) 
                                    THEN A.GIA_TRI_HACH_TOAN/NVL('|| DONVI_TIEN ||',1) ELSE 0 END),0) AS DTPT
                        ,NVL(SUM (CASE WHEN ( A.MA_TKTN LIKE ''82%'' and A.MA_CTMTQG like ''00000''  AND NOT A.MA_MUC  BETWEEN  ''9100'' AND ''9400'') THEN A.GIA_TRI_HACH_TOAN/NVL('|| DONVI_TIEN ||',1) ELSE 0 END),0) AS KPSN
                        ,NVL(SUM (CASE WHEN ( MA_TKTN like ''8%'' AND NOT  MA_CTMTQG like ''00000'' AND NOT  MA_MUC  BETWEEN  ''9100'' AND ''9400'') THEN A.GIA_TRI_HACH_TOAN/NVL('|| DONVI_TIEN ||',1) ELSE 0 END),0) AS CTX
                        
                        ,NVL(SUM (CASE WHEN ((( MA_TKTN like ''1513'' OR  MA_TKTN like ''1516'' OR  MA_TKTN like ''1523'' OR  MA_TKTN like ''1526'' OR  MA_TKTN like ''8113'' OR  MA_TKTN like ''8116'' OR  MA_TKTN like ''8123'' OR  MA_TKTN like ''8126'' OR  MA_TKTN like ''8953'' OR  MA_TKTN like ''8954'' OR  MA_TKTN like ''8955'' OR  MA_TKTN like ''8951'')  AND   ( MA_LOAI like ''340'' OR  MA_LOAI like ''581'' OR  MA_LOAI like ''610'' OR  MA_LOAI like ''640'')  AND  ( MA_MUC  BETWEEN  ''6000'' AND ''7250'' OR  MA_MUC like ''7750'' OR  MA_MUC like ''7850'' OR  MA_MUC like ''7900'' OR  MA_MUC like ''7950'' OR  MA_MUC like ''8000'' OR  MA_MUC like ''8150'' OR  MA_MUC like ''9000'' OR  MA_MUC like ''9050'' OR  MA_MUC  BETWEEN  ''9200'' AND ''9400'')  AND NOT   MA_CTMTQG like ''00956'')  OR  ( MA_TKTN like ''8959''  AND  ( MA_MUC  BETWEEN  ''6000'' AND ''7150'' OR  MA_MUC like ''7250'' OR  MA_MUC like ''7750'' OR  MA_MUC like ''7850'' OR  MA_MUC like ''7950'' OR  MA_MUC like ''8000'' OR  MA_MUC like ''8150'' OR  MA_MUC like ''9000'' OR  MA_MUC like ''9050'')  AND  ( MA_LOAI like ''340'' OR  MA_LOAI like ''581'' OR  MA_LOAI like ''610'' OR  MA_LOAI like ''640'')  AND NOT   MA_CTMTQG like ''00956'') OR  ( MA_TKTN like ''8993''  AND   MA_TIEUMUC like ''7551'')) 
                                    THEN A.GIA_TRI_HACH_TOAN/NVL('|| DONVI_TIEN ||',1) ELSE 0 END),0) AS CTX_KH
                        ,NVL(SUM (CASE WHEN ((((( MA_TKTN like ''1513'' OR  MA_TKTN like ''1516'' OR  MA_TKTN like ''1523'' OR  MA_TKTN like ''1526'' OR  MA_TKTN like ''8113'' OR  MA_TKTN like ''8116'' OR  MA_TKTN like ''8123'' OR  MA_TKTN like ''8126'' OR  MA_TKTN like ''8953'' OR  MA_TKTN like ''8954'' OR  MA_TKTN like ''8955'' OR  MA_TKTN like ''8951'')  AND    MA_LOAI like ''220''  AND  ( MA_MUC  BETWEEN  ''6000'' AND ''7250'' OR  MA_MUC like ''7750'' OR  MA_MUC like ''7850'' OR  MA_MUC like ''7900'' OR  MA_MUC like ''7950'' OR  MA_MUC like ''8000'' OR  MA_MUC like ''8150'' OR  MA_MUC like ''9000'' OR  MA_MUC like ''9050'' OR  MA_MUC  BETWEEN  ''9200'' AND ''9400'')  AND NOT   MA_CTMTQG like ''00956'') OR  ( MA_TKTN like ''8959''  AND  ( MA_MUC  BETWEEN  ''6000'' AND ''7150'' OR  MA_MUC like ''7250'' OR  MA_MUC like ''7750'' OR  MA_MUC like ''7850'' OR  MA_MUC like ''7950'' OR  MA_MUC like ''8000'' OR  MA_MUC like ''8150'' OR  MA_MUC like ''9000'' OR  MA_MUC like ''9050'')  AND   MA_LOAI like ''220''  AND NOT   MA_CTMTQG like ''00956'')) OR  ((( MA_TKTN like ''1513'' OR  MA_TKTN like ''1516'' OR  MA_TKTN like ''1523'' OR  MA_TKTN like ''1526'' OR  MA_TKTN like ''8113'' OR  MA_TKTN like ''8116'' OR  MA_TKTN like ''8123'' OR  MA_TKTN like ''8126'' OR  MA_TKTN like ''8953'' OR  MA_TKTN like ''8954'' OR  MA_TKTN like ''8955'' OR  MA_TKTN like ''8951'')  AND   ( MA_NGANHKT  BETWEEN  ''011'' AND ''021'' OR    MA_NGANHKT like ''024'')  AND  ( MA_MUC  BETWEEN  ''6000'' AND ''7250'' OR  MA_MUC like ''7750'' OR  MA_MUC like ''7850'' OR  MA_MUC like ''7900'' OR  MA_MUC like ''7950'' OR  MA_MUC like ''8000'' OR  MA_MUC like ''8150'' OR  MA_MUC like ''9000'' OR  MA_MUC like ''9050'' OR  MA_MUC  BETWEEN  ''9200'' AND ''9400'')  AND NOT   MA_CTMTQG like ''00956'')  OR (( MA_TKTN like ''1513'' OR  MA_TKTN like ''1516'' OR  MA_TKTN like ''1523'' OR  MA_TKTN like ''1526'' OR  MA_TKTN like ''8113'' OR  MA_TKTN like ''8116'' OR  MA_TKTN like ''8123'' OR  MA_TKTN like ''8126'' OR  MA_TKTN like ''8953'' OR  MA_TKTN like ''8954'' OR  MA_TKTN like ''8955'' OR  MA_TKTN like ''8951'')  AND    MA_CHUONG like ''012''  AND  ( MA_NGANHKT  BETWEEN  ''011'' AND ''021'' OR    MA_NGANHKT like ''024'')  AND   MA_MUC like ''8800''  AND NOT   MA_CTMTQG like ''00956'')  OR ( MA_TKTN like ''8959''  AND  ( MA_MUC  BETWEEN  ''6000'' AND ''7150'' OR  MA_MUC like ''7250'' OR  MA_MUC like ''7750'' OR  MA_MUC like ''7850'' OR  MA_MUC like ''7950'' OR  MA_MUC like ''8000'' OR  MA_MUC like ''8150'' OR  MA_MUC like ''9000'' OR  MA_MUC like ''9050'')  AND  ( MA_NGANHKT  BETWEEN  ''011'' AND ''021'' OR    MA_NGANHKT like ''024'')  AND NOT   MA_CTMTQG like ''00956'')  OR ( MA_TKTN like ''8959''  AND   MA_TIEUMUC like ''8052''  AND   MA_NGANHKT like ''016''  AND NOT   MA_CTMTQG like ''00956'')) OR  ((( MA_TKTN like ''1513'' OR  MA_TKTN like ''1516'' OR  MA_TKTN like ''1523'' OR  MA_TKTN like ''1526'' OR  MA_TKTN like ''8113'' OR  MA_TKTN like ''8116'' OR  MA_TKTN like ''8123'' OR  MA_TKTN like ''8126'' OR  MA_TKTN like ''8953'' OR  MA_TKTN like ''8954'' OR  MA_TKTN like ''8955'' OR  MA_TKTN like ''8951'')  AND   ((( MA_LOAI like ''040'' OR  MA_LOAI like ''070'' OR  MA_LOAI like ''130'' OR  MA_LOAI like ''160'' OR  MA_LOAI like ''190'' OR  MA_LOAI like ''250'' OR  MA_LOAI like ''310'' OR  MA_LOAI like ''400'' OR  MA_LOAI like ''430'') OR ( MA_NGANHKT like ''558'' OR  MA_NGANHKT like ''561'' OR  MA_NGANHKT like ''582'' OR  MA_NGANHKT like ''583''))  AND NOT   MA_NGANHKT  BETWEEN  ''252'' AND ''254'')  AND  ( MA_MUC  BETWEEN  ''6000'' AND ''7250'' OR  MA_MUC like ''7750'' OR  MA_MUC like ''7850'' OR  MA_MUC like ''7900'' OR  MA_MUC like ''7950'' OR  MA_MUC like ''8000'' OR  MA_MUC like ''8150'' OR  MA_MUC like ''9000'' OR  MA_MUC like ''9050'' OR  MA_MUC  BETWEEN  ''9200'' AND ''9400'')  AND NOT   MA_CTMTQG like ''00956'')  OR ( MA_TKTN like ''8959''  AND  ( MA_MUC  BETWEEN  ''6000'' AND ''7150'' OR  MA_MUC like ''7250'' OR  MA_MUC like ''7750'' OR  MA_MUC like ''7850'' OR  MA_MUC like ''7950'' OR  MA_MUC like ''8000'' OR  MA_MUC like ''8150'' OR  MA_MUC like ''9000'' OR  MA_MUC like ''9050'')  AND  ((( MA_LOAI like ''040'' OR  MA_LOAI like ''070'' OR  MA_LOAI like ''130'' OR  MA_LOAI like ''160'' OR  MA_LOAI like ''190'' OR  MA_LOAI like ''250'' OR  MA_LOAI like ''310'' OR  MA_LOAI like ''400'' OR  MA_LOAI like ''430'') OR ( MA_NGANHKT like ''558'' OR  MA_NGANHKT like ''561'' OR  MA_NGANHKT like ''582'' OR  MA_NGANHKT like ''583''))  AND NOT   MA_NGANHKT  BETWEEN  ''252'' AND ''254'')  AND NOT   MA_CTMTQG like ''00956'')))) 
                                  THEN A.GIA_TRI_HACH_TOAN/NVL('|| DONVI_TIEN ||',1) ELSE 0 END),0) AS CTX_HDKT
                       ,NVL(SUM (CASE WHEN ((( MA_TKTN like ''1513'' OR  MA_TKTN like ''1516'' OR  MA_TKTN like ''1523'' OR  MA_TKTN like ''1526'' OR  MA_TKTN like ''8113'' OR  MA_TKTN like ''8116'' OR  MA_TKTN like ''8123'' OR  MA_TKTN like ''8126'' OR  MA_TKTN like ''8953'' OR  MA_TKTN like ''8954'' OR  MA_TKTN like ''8955'' OR  MA_TKTN like ''8951'')  AND   ( MA_NGANHKT  BETWEEN  ''461'' AND ''467'' OR   MA_NGANHKT like ''472'' OR  MA_NGANHKT like ''473'' OR  MA_NGANHKT like ''489'')  AND  ( MA_MUC  BETWEEN  ''6000'' AND ''7250'' OR  MA_MUC like ''7750'' OR  MA_MUC like ''7850'' OR  MA_MUC like ''7900'' OR  MA_MUC like ''7950'' OR  MA_MUC like ''8000'' OR  MA_MUC like ''8150'' OR  MA_MUC like ''9000'' OR  MA_MUC like ''9050'' OR  MA_MUC  BETWEEN  ''9200'' AND ''9400'')  AND NOT   MA_CTMTQG like ''00956'')  OR  ( MA_TKTN like ''8959''  AND  ( MA_MUC  BETWEEN  ''6000'' AND ''7150'' OR  MA_MUC like ''7250'' OR  MA_MUC like ''7750'' OR  MA_MUC like ''7850'' OR  MA_MUC like ''7950'' OR  MA_MUC like ''8000'' OR  MA_MUC like ''8150'' OR  MA_MUC like ''9000'' OR  MA_MUC like ''9050'')  AND   MA_NGANHKT  BETWEEN  ''461'' AND ''467''  AND NOT   MA_CTMTQG like ''00956'')) 
                                 THEN A.GIA_TRI_HACH_TOAN/NVL('|| DONVI_TIEN ||',1) ELSE 0 END),0) AS CTX_QLNN
                        ,NVL(SUM (CASE WHEN ((( MA_TKTN like ''8959'' OR  MA_TKTN like ''1919'' OR  MA_TKTN like ''8919'') AND ( MA_CHUONG like ''009'' OR  MA_CHUONG like ''010'' OR  MA_CHUONG like ''035'' OR  MA_CHUONG like ''100'') AND  MA_NGANHKT like ''465'' AND NOT ( MA_MUC like ''7200'' OR  MA_MUC like ''7400'' OR  MA_MUC like ''9700'' OR  MA_MUC like ''9100'' OR  MA_MUC like ''9200'' OR  MA_MUC like ''9250'' OR  MA_MUC like ''9300'' OR  MA_MUC like ''9350'' OR  MA_MUC like ''9400'') AND NOT  MA_CTMTQG like ''00956'') OR   (( MA_TKTN like ''1513'' OR  MA_TKTN like ''1516'' OR  MA_TKTN like ''1523'' OR  MA_TKTN like ''1526'' OR  MA_TKTN like ''1912'' OR  MA_TKTN like ''8113'' OR  MA_TKTN like ''8116'' OR  MA_TKTN like ''8123'' OR  MA_TKTN like ''8126'' OR  MA_TKTN like ''8913'' OR  MA_TKTN like ''8953'' OR  MA_TKTN like ''8954'' OR  MA_TKTN like ''8955'' OR  MA_TKTN like ''8951'') AND ( MA_CHUONG like ''009'' OR  MA_CHUONG like ''010'' OR  MA_CHUONG like ''035'' OR  MA_CHUONG like ''100'') AND  MA_NGANHKT like ''465'' AND NOT ( MA_MUC like ''7200'' OR  MA_MUC like ''7400'' OR  MA_MUC like ''9700'')  AND NOT  MA_CTMTQG like ''00956'')) 
                                THEN A.GIA_TRI_HACH_TOAN/NVL('|| DONVI_TIEN ||',1) ELSE 0 END),0) AS CTX_DB
                        ,NVL(SUM (CASE WHEN (( MA_TKTN like ''1513'' OR  MA_TKTN like ''1516'' OR  MA_TKTN like ''1523'' OR  MA_TKTN like ''1526'' OR  MA_TKTN like ''1912'' OR  MA_TKTN like ''8113'' OR  MA_TKTN like ''8116'' OR  MA_TKTN like ''8123'' OR  MA_TKTN like ''8126'' OR  MA_TKTN like ''8913'' OR  MA_TKTN like ''8953'' OR  MA_TKTN like ''8954'' OR  MA_TKTN like ''8955'' OR  MA_TKTN like ''8951'') AND  MA_MUC like ''7200'' AND NOT  MA_CTMTQG like ''00956'') 
                                THEN A.GIA_TRI_HACH_TOAN/NVL('|| DONVI_TIEN ||',1) ELSE 0 END),0) AS CTX_TG
                        ,NVL(SUM (CASE WHEN ((( MA_TKTN like ''1513'' OR  MA_TKTN like ''1516'' OR  MA_TKTN like ''1523'' OR  MA_TKTN like ''1526'' OR  MA_TKTN like ''8113'' OR  MA_TKTN like ''8116'' OR  MA_TKTN like ''8123'' OR  MA_TKTN like ''8126'' OR  MA_TKTN like ''8953'' OR  MA_TKTN like ''8954'' OR  MA_TKTN like ''8955'' OR  MA_TKTN like ''8951'')  AND   MA_LOAI like ''280''  AND  ( MA_MUC  BETWEEN  ''6000'' AND ''7250'' OR  MA_MUC like ''7750'' OR  MA_MUC like ''7850'' OR  MA_MUC like ''7900'' OR  MA_MUC like ''7950'' OR  MA_MUC like ''8000'' OR  MA_MUC like ''8150'' OR  MA_MUC like ''9000'' OR  MA_MUC like ''9050'' OR  MA_MUC  BETWEEN  ''9200'' AND ''9400'')  AND NOT   MA_CTMTQG like ''00956'')  OR  ( MA_TKTN like ''8959''  AND  ( MA_MUC  BETWEEN  ''6000'' AND ''7150'' OR  MA_MUC like ''7250'' OR  MA_MUC like ''7750'' OR  MA_MUC like ''7850'' OR  MA_MUC like ''7950'' OR  MA_MUC like ''8000'' OR  MA_MUC like ''8150'' OR  MA_MUC like ''9000'' OR  MA_MUC like ''9050'')  AND   MA_LOAI like ''280''  AND NOT   MA_CTMTQG like ''00956'')) 
                                THEN A.GIA_TRI_HACH_TOAN/NVL('|| DONVI_TIEN ||',1) ELSE 0 END),0) AS CTX_BVMT
                        ,NVL(SUM (CASE WHEN ((( MA_TKTN like ''1513'' OR  MA_TKTN like ''1516'' OR  MA_TKTN like ''1523'' OR  MA_TKTN like ''1526'' OR  MA_TKTN like ''1912'' OR  MA_TKTN like ''8113'' OR  MA_TKTN like ''8116'' OR  MA_TKTN like ''8123'' OR  MA_TKTN like ''8126'' OR  MA_TKTN like ''8913'' OR  MA_TKTN like ''8953'' OR  MA_TKTN like ''8954'' OR  MA_TKTN like ''8955'' OR  MA_TKTN like ''8951'') AND ( MA_NGANHKT like ''474'' OR  MA_NGANHKT like ''524'' OR  MA_NGANHKT like ''525'' OR  MA_NGANHKT like ''527'' OR  MA_NGANHKT like ''528'' OR  MA_NGANHKT like ''531'') AND ( MA_MUC  BETWEEN  ''6000'' AND ''7150'' OR  MA_MUC like ''7250'' OR  MA_MUC like ''7750'' OR  MA_MUC like ''7850'' OR  MA_MUC like ''7900'' OR  MA_MUC like ''7950'' OR  MA_MUC like ''8000'' OR  MA_MUC like ''8150'' OR  MA_MUC like ''9000'' OR  MA_MUC like ''9050'' OR  MA_MUC like ''9200'' OR  MA_MUC like ''9250'' OR  MA_MUC like ''9300'' OR  MA_MUC like ''9350'' OR  MA_MUC like ''9400'' OR  MA_MUC like ''8050'' OR  MA_MUC like ''8100'' OR  MA_MUC like ''8750'' OR  MA_MUC like ''8800'' OR  MA_MUC like ''8950'') AND NOT ( MA_CTMTQG like ''00956'' OR  MA_CTMTQG like ''00030''))  OR   ( ( MA_TKTN like ''8959'' OR  MA_TKTN like ''1919'' OR  MA_TKTN like ''8919'') AND ( MA_NGANHKT like ''474'' OR  MA_NGANHKT like ''524'' OR  MA_NGANHKT like ''525'' OR  MA_NGANHKT like ''527'' OR  MA_NGANHKT like ''528'' OR  MA_NGANHKT like ''531'') AND ( MA_MUC  BETWEEN  ''6000'' AND ''7150'' OR  MA_MUC like ''7250'' OR  MA_MUC like ''7750'' OR  MA_MUC like ''7850'' OR  MA_MUC like ''7900'' OR  MA_MUC like ''7950'' OR  MA_MUC like ''8000'' OR  MA_MUC like ''8150'' OR  MA_MUC like ''9000'' OR  MA_MUC like ''9050'') AND NOT ( MA_CTMTQG like ''00956'' OR  MA_CTMTQG like ''00030'') )) 
                                THEN A.GIA_TRI_HACH_TOAN/NVL('|| DONVI_TIEN ||',1) ELSE 0 END),0) AS CTX_BDXH
                        ,NVL(SUM (CASE WHEN ((( MA_TKTN like ''1523'' OR  MA_TKTN like ''1511'' OR  MA_TKTN like ''1521'' OR  MA_TKTN like ''1912'' OR  MA_TKTN like ''81%'' OR  MA_TKTN like ''8913'' OR  MA_TKTN like ''8953'' OR  MA_TKTN like ''8954'' OR  MA_TKTN like ''8955'' OR  MA_TKTN like ''8951'')  AND  ( MA_NGANHKT like ''471'')  AND NOT  ( MA_MUC like ''7200'' OR  MA_MUC like ''7400'' OR  MA_MUC like ''9700'')) OR  ( MA_TKTN like ''8959'' OR  MA_TKTN like ''1919'' OR  MA_TKTN like ''8919'')  AND  ( MA_NGANHKT like ''471'')  AND NOT  ( MA_MUC like ''7200'' OR  MA_MUC like ''7400'' OR  MA_MUC like ''9700'' OR  MA_MUC like ''9100'' OR   MA_MUC  BETWEEN  ''9200'' AND ''9400'')) 
                                THEN A.GIA_TRI_HACH_TOAN/NVL('|| DONVI_TIEN ||',1) ELSE 0 END),0) AS CTX_AN
                        ,NVL(SUM (CASE WHEN ((( MA_TKTN like ''8959'' OR  MA_TKTN like ''1919'' OR  MA_TKTN like ''8919'') AND ( MA_LOAI like ''550'') AND NOT ( MA_NGANHKT like ''558'' OR  MA_NGANHKT like ''561'' OR  MA_NGANHKT like ''562'') AND ( MA_MUC  BETWEEN  ''6000'' AND ''7150'' OR  MA_MUC like ''7250'' OR  MA_MUC like ''7750'' OR  MA_MUC like ''7850'' OR  MA_MUC like ''7900'' OR  MA_MUC like ''7950'' OR  MA_MUC like ''8000'' OR  MA_MUC like ''8150'' OR  MA_MUC like ''9000'' OR  MA_MUC like ''9050'')  AND NOT   MA_CTMTQG like ''00956'') OR   (( MA_TKTN like ''1513'' OR  MA_TKTN like ''1516'' OR  MA_TKTN like ''1523'' OR  MA_TKTN like ''1526'' OR  MA_TKTN like ''1912'' OR  MA_TKTN like ''8113'' OR  MA_TKTN like ''8116'' OR  MA_TKTN like ''8123'' OR  MA_TKTN like ''8126'' OR  MA_TKTN like ''8913'' OR  MA_TKTN like ''8953'' OR  MA_TKTN like ''8954'' OR  MA_TKTN like ''8955'' OR  MA_TKTN like ''8951'') AND  MA_LOAI like ''550'' AND NOT ( MA_NGANHKT like ''558'' OR  MA_NGANHKT like ''561'' OR  MA_NGANHKT like ''562'') AND ( MA_MUC  BETWEEN  ''6000'' AND ''7150'' OR  MA_MUC like ''7250'' OR  MA_MUC like ''7750'' OR  MA_MUC like ''7850'' OR  MA_MUC like ''7900'' OR  MA_MUC like ''7950'' OR  MA_MUC like ''8000'' OR  MA_MUC like ''8150'' OR  MA_MUC like ''9000'' OR  MA_MUC like ''9050'' OR  MA_MUC like ''9200'' OR  MA_MUC like ''9250'' OR  MA_MUC like ''9300'' OR  MA_MUC like ''9350'' OR  MA_MUC like ''9400'' OR  MA_MUC like ''8050'' OR  MA_MUC like ''8100'' OR  MA_MUC like ''8750'' OR  MA_MUC like ''8800'' OR  MA_MUC like ''8950'') AND NOT  MA_CTMTQG like ''00956''))
                                THEN A.GIA_TRI_HACH_TOAN/NVL('|| DONVI_TIEN ||',1) ELSE 0 END),0) AS CTX_VH                                             
                        ,NVL(SUM (CASE WHEN ((( MA_TKTN like ''1513'' OR  MA_TKTN like ''1516'' OR  MA_TKTN like ''1523'' OR  MA_TKTN like ''1526'' OR  MA_TKTN like ''1912'' OR  MA_TKTN like ''8113'' OR  MA_TKTN like ''8116'' OR  MA_TKTN like ''8123'' OR  MA_TKTN like ''8126'' OR  MA_TKTN like ''8913'' OR  MA_TKTN like ''8953'' OR  MA_TKTN like ''8954'' OR  MA_TKTN like ''8955'' OR  MA_TKTN like ''8951'') AND ( MA_LOAI like ''370'') AND ( MA_MUC  BETWEEN  ''6000'' AND ''7150'' OR  MA_MUC like ''7250'' OR  MA_MUC like ''7750'' OR  MA_MUC like ''7850'' OR  MA_MUC like ''7900'' OR  MA_MUC like ''7950'' OR  MA_MUC like ''8000'' OR  MA_MUC like ''8150'' OR  MA_MUC like ''9000'' OR  MA_MUC like ''9050'' OR  MA_MUC like ''9200'' OR  MA_MUC like ''9250'' OR  MA_MUC like ''9300'' OR  MA_MUC like ''9350'' OR  MA_MUC like ''9400'' OR  MA_MUC like ''8050'' OR  MA_MUC like ''8100'' OR  MA_MUC like ''8750'' OR  MA_MUC like ''8800'' OR  MA_MUC like ''8950'') AND NOT  MA_CTMTQG like ''00956'') OR  (( MA_TKTN like ''8959'' OR  MA_TKTN like ''1919'' OR  MA_TKTN like ''8919'') AND ( MA_LOAI like ''370'') AND ( MA_MUC  BETWEEN  ''6000'' AND ''7150'' OR  MA_MUC like ''7250'' OR  MA_MUC like ''7750'' OR  MA_MUC like ''7850'' OR  MA_MUC like ''7900'' OR  MA_MUC like ''7950'' OR  MA_MUC like ''8000'' OR  MA_MUC like ''8150'' OR  MA_MUC like ''9000'' OR  MA_MUC like ''9050'')	 AND NOT  MA_CTMTQG like ''00956'' )) 
                                THEN A.GIA_TRI_HACH_TOAN/NVL('|| DONVI_TIEN ||',1) ELSE 0 END),0) AS CTX_KHCN                        
                        ,NVL(SUM (CASE WHEN ((( MA_TKTN like ''8959'' OR  MA_TKTN like ''1919'' OR  MA_TKTN like ''8919'') AND   MA_NGANHKT like ''468''  AND NOT ( MA_MUC like ''7200'' OR  MA_MUC like ''7400'' OR  MA_MUC like ''9700'' OR  MA_MUC like ''9100'' OR  MA_MUC like ''9200'' OR  MA_MUC like ''9250'' OR  MA_MUC like ''9300'' OR  MA_MUC like ''9350'' OR  MA_MUC like ''9400'')  AND NOT   MA_CTMTQG like ''00956'' ) OR  (( MA_TKTN like ''1513'' OR  MA_TKTN like ''1516'' OR  MA_TKTN like ''1523'' OR  MA_TKTN like ''1526'' OR  MA_TKTN like ''1912'' OR  MA_TKTN like ''8113'' OR  MA_TKTN like ''8116'' OR  MA_TKTN like ''8123'' OR  MA_TKTN like ''8126'' OR  MA_TKTN like ''8913'' OR  MA_TKTN like ''8953'' OR  MA_TKTN like ''8954'' OR  MA_TKTN like ''8955'' OR  MA_TKTN like ''8951'')  AND   ( MA_NGANHKT like ''468'') AND NOT ( MA_MUC like ''7200'' OR  MA_MUC like ''7400'' OR  MA_MUC like ''9700'')  AND NOT   MA_CTMTQG like ''00956'')) 
                                THEN A.GIA_TRI_HACH_TOAN/NVL('|| DONVI_TIEN ||',1) ELSE 0 END),0) AS CTX_QP
                        ,NVL(SUM (CASE WHEN ((( MA_TKTN like ''1513'' OR  MA_TKTN like ''1516'' OR  MA_TKTN like ''1523'' OR  MA_TKTN like ''1526'' OR  MA_TKTN like ''8113'' OR  MA_TKTN like ''8116'' OR  MA_TKTN like ''8123'' OR  MA_TKTN like ''8126'' OR  MA_TKTN like ''8953'' OR  MA_TKTN like ''8954'' OR  MA_TKTN like ''8955'' OR  MA_TKTN like ''8951'')  AND   MA_LOAI like ''490'' AND  ( MA_MUC  BETWEEN  ''6000'' AND ''7250'' OR  MA_MUC like ''7750'' OR  MA_MUC like ''7850'' OR  MA_MUC like ''7900'' OR  MA_MUC like ''7950'' OR  MA_MUC like ''8000'' OR  MA_MUC like ''8150'' OR  MA_MUC like ''9000'' OR  MA_MUC like ''9050'' OR  MA_MUC  BETWEEN  ''9200'' AND ''9400'')  AND NOT   MA_CTMTQG like ''00956'')) 
                                THEN A.GIA_TRI_HACH_TOAN/NVL('|| DONVI_TIEN ||',1) ELSE 0 END),0) AS CTX_GD
                        ,NVL(SUM (CASE WHEN ((( MA_TKTN like ''1513'' OR  MA_TKTN like ''1516'' OR  MA_TKTN like ''1523'' OR  MA_TKTN like ''1526'' OR  MA_TKTN like ''8113'' OR  MA_TKTN like ''8116'' OR  MA_TKTN like ''8123'' OR  MA_TKTN like ''8126'' OR  MA_TKTN like ''8953'' OR  MA_TKTN like ''8954'' OR  MA_TKTN like ''8955'' OR  MA_TKTN like ''8951'')  AND  ( MA_LOAI like ''520''  AND NOT ( MA_NGANHKT like ''524'' OR  MA_NGANHKT like ''525'' OR  MA_NGANHKT like ''527'' OR  MA_NGANHKT like ''528'' OR  MA_NGANHKT like ''531'')) AND  ( MA_MUC  BETWEEN  ''6000'' AND ''7250'' OR  MA_MUC like ''7750'' OR  MA_MUC like ''7850'' OR  MA_MUC like ''7900'' OR  MA_MUC like ''7950'' OR  MA_MUC like ''8000'' OR  MA_MUC like ''8150'' OR  MA_MUC like ''9000'' OR  MA_MUC like ''9050'' OR  MA_MUC  BETWEEN  ''9200'' AND ''9400'')  AND NOT   MA_CTMTQG like ''00956'')  OR  ( MA_TKTN like ''8959''  AND  ( MA_MUC  BETWEEN  ''6000'' AND ''7150'' OR  MA_MUC like ''7250'' OR  MA_MUC like ''7750'' OR  MA_MUC like ''7850'' OR  MA_MUC like ''7950'' OR  MA_MUC like ''8000'' OR  MA_MUC like ''8150'' OR  MA_MUC like ''9000'' OR  MA_MUC like ''9050'')  AND  ( MA_LOAI like ''520''  AND NOT ( MA_NGANHKT like ''524'' OR  MA_NGANHKT like ''525'' OR  MA_NGANHKT like ''527'' OR  MA_NGANHKT like ''528'' OR  MA_NGANHKT like ''531''))  AND NOT   MA_CTMTQG like ''00956''))
                                THEN A.GIA_TRI_HACH_TOAN/NVL('|| DONVI_TIEN ||',1) ELSE 0 END),0) AS CTX_YT
                        ,NVL(SUM (CASE WHEN ((( MA_TKTN like ''1513'' OR  MA_TKTN like ''1516'' OR  MA_TKTN like ''1523'' OR  MA_TKTN like ''1526'' OR  MA_TKTN like ''8113'' OR  MA_TKTN like ''8116'' OR  MA_TKTN like ''8123'' OR  MA_TKTN like ''8126'' OR  MA_TKTN like ''8953'' OR  MA_TKTN like ''8954'' OR  MA_TKTN like ''8955'' OR  MA_TKTN like ''8951'')  AND   MA_NGANHKT like ''562''  AND  ( MA_MUC  BETWEEN  ''6000'' AND ''7250'' OR  MA_MUC like ''7750'' OR  MA_MUC like ''7850'' OR  MA_MUC like ''7900'' OR  MA_MUC like ''7950'' OR  MA_MUC like ''8000'' OR  MA_MUC like ''8150'' OR  MA_MUC like ''9000'' OR  MA_MUC like ''9050'' OR  MA_MUC  BETWEEN  ''9200'' AND ''9400'')  AND NOT   MA_CTMTQG like ''00956'')  OR  ( MA_TKTN like ''8959''  AND  ( MA_MUC  BETWEEN  ''6000'' AND ''7150'' OR  MA_MUC like ''7250'' OR  MA_MUC like ''7750'' OR  MA_MUC like ''7850'' OR  MA_MUC like ''7950'' OR  MA_MUC like ''8000'' OR  MA_MUC like ''8150'' OR  MA_MUC like ''9000'' OR  MA_MUC like ''9050'')  AND   MA_NGANHKT like ''562''  AND NOT   MA_CTMTQG like ''00956''))
                                THEN A.GIA_TRI_HACH_TOAN/NVL('|| DONVI_TIEN ||',1) ELSE 0 END),0) AS CTX_TDTT
                        ,NVL(SUM (CASE WHEN ((( MA_TKTN like ''1513'' OR  MA_TKTN like ''1516'' OR  MA_TKTN like ''1523'' OR  MA_TKTN like ''1526'' OR  MA_TKTN like ''1912'' OR  MA_TKTN like ''8113'' OR  MA_TKTN like ''8116'' OR  MA_TKTN like ''8123'' OR  MA_TKTN like ''8126'' OR  MA_TKTN like ''8913'' OR  MA_TKTN like ''8953'' OR  MA_TKTN like ''8954'' OR  MA_TKTN like ''8955'' OR  MA_TKTN like ''8951'') AND ( MA_NGANHKT like ''252'' OR  MA_NGANHKT like ''253'' OR  MA_NGANHKT like ''254'') AND ( MA_MUC  BETWEEN  ''6000'' AND ''7150'' OR  MA_MUC like ''7250'' OR  MA_MUC like ''7750'' OR  MA_MUC like ''7850'' OR  MA_MUC like ''7900'' OR  MA_MUC like ''7950'' OR  MA_MUC like ''8000'' OR  MA_MUC like ''8150'' OR  MA_MUC like ''9000'' OR  MA_MUC like ''9050'' OR  MA_MUC like ''9200'' OR  MA_MUC like ''9250'' OR  MA_MUC like ''9300'' OR  MA_MUC like ''9350'' OR  MA_MUC like ''9400'' OR  MA_MUC like ''8050'' OR  MA_MUC like ''8100'' OR  MA_MUC like ''8750'' OR  MA_MUC like ''8800'' OR  MA_MUC like ''8950'') AND NOT  MA_CTMTQG like ''00956'') OR  ( ( MA_TKTN like ''8959'' OR  MA_TKTN like ''1919'' OR  MA_TKTN like ''8919'') AND ( MA_NGANHKT like ''252'' OR  MA_NGANHKT like ''253'' OR  MA_NGANHKT like ''254'') AND ( MA_MUC  BETWEEN  ''6000'' AND ''7150'' OR  MA_MUC like ''7250'' OR  MA_MUC like ''7750'' OR  MA_MUC like ''7850'' OR  MA_MUC like ''7900'' OR  MA_MUC like ''7950'' OR  MA_MUC like ''8000'' OR  MA_MUC like ''8150'' OR  MA_MUC like ''9000'' OR  MA_MUC like ''9050'') AND NOT  MA_CTMTQG like ''00956'' )) 
                                THEN A.GIA_TRI_HACH_TOAN/NVL('|| DONVI_TIEN ||',1) ELSE 0 END),0) AS CTX_PT
                                
                        ,NVL(SUM (CASE WHEN ( NOT A.MA_CTMTQG like ''00000'' AND NOT  A.MA_CTMTQG like ''0027%'' AND NOT A.MA_CTMTQG like ''050%'') THEN A.GIA_TRI_HACH_TOAN/NVL('|| DONVI_TIEN ||',1) ELSE 0 END),0) AS TSBS
                        ,NVL(SUM (CASE WHEN ( NOT A.MA_CTMTQG like ''00000'' AND NOT  A.MA_CTMTQG like ''0027%'' AND NOT A.MA_CTMTQG like ''050%''  AND A.MA_MUC  BETWEEN  ''9100'' AND ''9400'') THEN A.GIA_TRI_HACH_TOAN/NVL('|| DONVI_TIEN ||',1) ELSE 0 END),0) AS TSCDT
                        
                        ,NVL(SUM (CASE WHEN ( NOT A.MA_CTMTQG like ''00000'' AND NOT  A.MA_CTMTQG like ''0027%'' AND NOT A.MA_CTMTQG like ''050%''  AND A.MA_MUC  BETWEEN  ''9100'' AND ''9400'' AND  A.MA_NGUON_NSNN like ''01'') THEN A.GIA_TRI_HACH_TOAN/NVL('|| DONVI_TIEN ||',1) ELSE 0 END),0) AS CDTVTN
                        
                        
                        
                        
                        
                        ,NVL(SUM (CASE WHEN ( NOT A.MA_CTMTQG like ''00000'' AND NOT  A.MA_CTMTQG like ''0027%'' AND NOT A.MA_CTMTQG like ''050%''  AND A.MA_MUC  BETWEEN  ''9100'' AND ''9400'' AND  A.MA_NGUON_NSNN like ''50'') THEN A.GIA_TRI_HACH_TOAN/NVL('|| DONVI_TIEN ||',1) ELSE 0 END),0) AS CDTNVN
                        ,NVL(SUM (CASE WHEN ( NOT A.MA_CTMTQG like ''00000'' AND NOT  A.MA_CTMTQG like ''0027%'' AND NOT A.MA_CTMTQG like ''050%''  AND NOT A.MA_MUC  BETWEEN  ''9100'' AND ''9400'') THEN A.GIA_TRI_HACH_TOAN/NVL('|| DONVI_TIEN ||',1) ELSE 0 END),0) AS TSKP
                        ,NVL(SUM (CASE WHEN ( NOT A.MA_CTMTQG like ''00000'' AND NOT  A.MA_CTMTQG like ''0027%'' AND NOT A.MA_CTMTQG like ''050%''  AND NOT A.MA_MUC  BETWEEN  ''9100'' AND ''9400'' AND   A.MA_NGUON_NSNN like ''01'') THEN A.GIA_TRI_HACH_TOAN/NVL('|| DONVI_TIEN ||',1) ELSE 0 END),0) AS KPVTN
                        ,NVL(SUM (CASE WHEN ( NOT A.MA_CTMTQG like ''00000'' AND NOT  A.MA_CTMTQG like ''0027%'' AND NOT A.MA_CTMTQG like ''050%''  AND NOT A.MA_MUC  BETWEEN  ''9100'' AND ''9400''  AND   A.MA_NGUON_NSNN like ''50'') THEN A.GIA_TRI_HACH_TOAN/NVL('|| DONVI_TIEN ||',1) ELSE 0 END),0) AS KPVNN
                        FROM PHA_HACHTOAN_CHI A
                        WHERE A.NGAY_HIEU_LUC >= TO_DATE ('''|| '0101' || P_NAM  ||''', ''ddMMyyyy'')
                        AND A.NGAY_HIEU_LUC <= TO_DATE ('''|| '3112' || P_NAM  ||''', ''ddMMyyyy'')
                        AND NOT A.MA_CAP like ''1'' '
                        || TEMP
                        || P_CT
                        || ' GROUP BY A.MA_CHUONG,A.TEN_CHUONG
                        ) HT
                        WHERE 1=1 ORDER BY HT.MA_CHUONG';

  -- DBMS_OUTPUT.put_line (QUERY_STR);
BEGIN
EXECUTE IMMEDIATE QUERY_STR;
OPEN cur FOR QUERY_STR;
EXCEPTION
   WHEN NO_DATA_FOUND
   THEN
      DBMS_OUTPUT.put_line ('<your message>' || SQLERRM);
   WHEN OTHERS
   THEN
   DBMS_OUTPUT.put_line ('<your message>' || SQLERRM);
    --  DBMS_OUTPUT.put_line (QUERY_STR  || SQLERRM); 
END;    
END PHA_NS_BM61_ND31_TH;