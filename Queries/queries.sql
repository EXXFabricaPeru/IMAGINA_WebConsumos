CREATE PROCEDURE "EXX_SCT_Almacen_Etapa_Listar"
(
	IN P_Sucursal int,
	IN P_ETAPA varchar(15)
)
AS
BEGIN
	
	SELECT	"WhsCode", 
			"WhsCode" || '-'|| "WhsName" AS "WhsName"
	FROM "OWHS"
	WHERE /* "U_EXX_TPED_APTP" ='Y'
	  AND */ 
	  "BPLid" = :P_Sucursal
	  AND "U_EXC_ETAPA" = :P_ETAPA
	 ORDER BY "WhsCode";
END;

CREATE PROCEDURE "EXX_SCT_ArticuloVenta_Listar"
(
	IN P_Valor varchar(20), 
	IN P_Almacen varchar(20),
	IN P_ListPrice int,
	IN P_Moneda varchar(5),
	IN P_Todos int
)
AS
BEGIN
	
	SELECT	"itm"."ItemCode",
			"itm"."ItemName",
			IFNULL(CASE WHEN "pre"."Currency" = :P_Moneda THEN "pre"."Price" ELSE "pre"."AddPrice1" END, 0) AS "PriceUnit",
			"stk"."WhsCode",
			"stk"."OnHand",
			("stk"."OnHand" - "stk"."IsCommited" + "stk"."OnOrder") AS "Disponible",
			0 "UomEntry",
			"itm"."SalUnitMsr",
			'' "U_EXK_CENCOSTO",
			IFNULL("pre"."Currency", '') "Currency",
			"pre"."PriceList",
			"stk"."OnOrder" AS "Solicitado"
	FROM "OITM" "itm" 
	--INNER JOIN "OUOM" "und" ON "und"."UomName" = "itm"."SalUnitMsr"
	INNER JOIN "ITM1" "pre" ON "itm"."ItemCode" = "pre"."ItemCode" 
	--  	   AND "pre"."UomEntry" = "und"."UomEntry"	  	   
	INNER JOIN "OITW" "stk" ON "itm"."ItemCode" = "stk"."ItemCode"
	WHERE /* (("stk"."OnHand" - "stk"."OnOrder") > 0 OR 1 = :P_Todos)
	AND */ "itm"."SellItem" = 'Y'	  
	  AND "itm"."validFor" = 'Y'
	  AND "itm"."frozenFor" = 'N'
	  AND ("pre"."PriceList" = :P_ListPrice)
	  --AND ("pre"."Currency" = :P_Moneda)
	  AND (	UPPER("itm"."ItemCode") LIKE '%' || UPPER(:P_Valor) || '%' OR 
	  		UPPER("itm"."ItemName") LIKE '%' || UPPER(:P_Valor) || '%')
	  AND ("stk"."WhsCode" = :P_Almacen /*OR 1 = :P_Todos*/)
	ORDER BY 1;
END;

CREATE PROCEDURE "EXX_SCT_ClienteSucursal_Listar"
(
	IN P_Sucursal int
)
AS
BEGIN
	
	SELECT	T1."CardCode",
			T1."CardName"
	FROM "OBPL" T0
	INNER JOIN "OCRD" T1 ON T0."GlblLocNum" = T1."LicTradNum" AND T1."CardType" = 'C'
	WHERE T0."BPLId" = :P_Sucursal;
END;

CREATE PROCEDURE "EXX_SCT_CondicionPago_Listar"
	
AS
BEGIN

	SELECT "GroupNum" "Codigo", 
		   "PymntGroup" "Descripcion"
	FROM OCTG;
END;

CREATE PROCEDURE "EXX_SCT_Configuracion"
	-- Add the parameters for the stored procedure here	
AS
BEGIN
	
	SELECT	"Code",
			"U_EXX_CAMPO" "Campo",
			"U_EXX_FORMULARIO" "Formulario",
			"U_EXX_VISIBLE" "Visible",
			"U_EXX_EDITABLE" "Editable",
			"U_EXX_VALOR" "Valor"
	FROM "@EXX_SCT_CONFIG";
END;

CREATE PROCEDURE "EXX_SCT_Dimenciones_Listar"
	
AS
BEGIN
	
	SELECT	"DimCode",
			RPAD("DimDesc", 50, ' ') "DimDesc",
			"DimName"
	FROM ODIM
	ORDER BY "DimCode";
END;

CREATE PROCEDURE "EXX_SCT_Empleado_Listar" 
(
	IN P_Codigo int
)
LANGUAGE SQLSCRIPT AS
BEGIN
	
    -- Insert statements for procedure here
	SELECT	"empID"	"SlpCode",
			"lastName" || ' ' || "firstName" || ' ' || IFNULL("middleName",'') "SlpName"
	FROM "OHEM" T0
	WHERE "Active" = 'Y'
	  AND NOT EXISTS (SELECT 1 FROM "@EXX_SCT_USER" T1 WHERE T1."U_EXX_EMPLEADO" = T0."empID")
	
	UNION
	
	SELECT	"empID"	"SlpCode",
			"lastName" || ' ' || "firstName" || ' ' || IFNULL("middleName",'') "SlpName"
	FROM "OHEM" T0
	WHERE "empID" = :P_Codigo
	  
	ORDER BY 2;
END;

CREATE PROCEDURE "EXX_SCT_Login"
(
	IN P_User varchar(15),
	IN P_Pass varchar(150)
)
AS
BEGIN
	
	SELECT 	"DocEntry",
			"U_EXX_USER" "Usuario",
			"lastName" || ' ' || "firstName" || ' ' || IFNULL("middleName",'') "Nombre",
			COALESCE("U_EXX_EMPLEADO",0) "CodVendedor",
		/*	"U_EXX_PRICELIST" */ 1 "ListaPrecio",
			"U_EXX_MONEDA" "Moneda",
			COALESCE(T0."U_EXX_SUCURSAL",'0') "Sucursal",
		/*	COALESCE(T2."IsGrossPrc", '') */ 'N' "IsGrossPrc",
			0 "DiasVenc",
			U_EXX_PASS,
			U_EXX_PERFIL 
	FROM "@EXX_SCT_USER" T0
	LEFT JOIN OHEM T1 ON T0."U_EXX_EMPLEADO" = T1."empID"
	--LEFT JOIN OPLN T2 ON T0."U_EXX_PRICELIST" = T2."ListNum"
	WHERE "U_EXX_ACTIVO" = 'Y'
	  AND "U_EXX_USER" = :P_User
	  AND "U_EXX_PASS" = :P_Pass;
	  
END;

CREATE PROCEDURE "EXX_SCT_Moneda_Listar"
	
AS
BEGIN
	
	SELECT	"CurrCode" As "Codigo", 
			"CurrCode" ||' - '|| "CurrName" As "Descripcion" 
	FROM OCRN
	WHERE "Locked" = 'N';
END;

CREATE PROCEDURE "EXX_SCT_MotivoAnulacion_Listar"
	
AS
BEGIN
	
	SELECT	*
	FROM "@EXC_MOTVANU"
	ORDER BY "Code";
END;

CREATE PROCEDURE "EXX_SCT_MotivoAnulacion_Listar"
	
AS
BEGIN
	
	SELECT	*
	FROM "@EXC_MOTVANU"
	ORDER BY "Code";
END;

CREATE PROCEDURE "EXX_SCT_OrdenVenta_Buscar"
(
	IN P_DocEntry int
)
AS
BEGIN


	SELECT	"pec"."DocEntry",
			"pec"."DocNum",
			"pec"."DocDueDate",
			"pec"."DocDate",
			"pec"."TaxDate",
			"pec"."CardCode",
			"pec"."CardName",
			"pec"."ShipToCode",
			"pec"."Address2",
			"pec"."GroupNum",
			"pec"."Comments",
			"pec"."DocCur",
			"pec"."BPLId",
			"pec"."Series",
			"pec"."U_EXX_TIPOOPER" "TpoOperacion",
			(SELECT X."Name" FROM "@EXC_MOTVANU" X WHERE X."Code" = "pec".U_EXC_MOTANU) AS U_EXC_MOTANU,
			"ped"."LineNum",
			"ped"."ItemCode",
			"ped"."Dscription",
			"ped"."PriceBefDi" "Price",
			"ped"."Quantity",
			--"ped"."OpenQty",
			CASE WHEN 
			(SELECT COALESCE(SUM("end"."Quantity"),0) FROM DLN1 "end" 
			INNER JOIN ODLN "enc" ON "enc"."DocEntry"="end"."DocEntry"  
			WHERE "enc"."CANCELED" = 'N' 
			AND "end"."BaseType"= "pec"."ObjType" AND "end"."BaseEntry"= "ped"."DocEntry" AND "end"."BaseLine"= "ped"."LineNum")
			>= "ped"."Quantity"
			THEN
				0
			ELSE
				"ped"."Quantity"-
				(SELECT COALESCE(SUM("end"."Quantity"),0) FROM DLN1 "end" 
				INNER JOIN ODLN "enc" ON "enc"."DocEntry"="end"."DocEntry"  
				WHERE "enc"."CANCELED" = 'N' 
				AND "end"."BaseType"= "pec"."ObjType" AND "end"."BaseEntry"= "ped"."DocEntry" AND "end"."BaseLine"= "ped"."LineNum")
			END 
			"OpenQty",			
			CASE WHEN "pec"."DocCur" = 'SOL' THEN "ped"."LineTotal" ELSE "ped"."TotalFrgn" END"LineTotal",
			"pec"."TrnspCode",
			"pec"."DiscPrcnt" "Descuento",
			CASE WHEN "pec"."CANCELED" = 'Y' THEN 'A'
				 ELSE "pec"."DocStatus"
			END  "DocStatus",
			"ped"."TaxCode",
			"ped"."UomCode",
			"ped"."UomEntry",
			"ped"."WhsCode",
			"ped"."Project",
			"ped"."OcrCode",
			"ped"."OcrCode2",
			"ped"."OcrCode3",
			"ped"."OcrCode4",
			"ped"."OcrCode5",
			"ped"."DiscPrcnt",
			"ped".U_EXC_PARTPRES "CodPartida",
			"ped".U_EXC_PARTPRES || '-' || "ped".U_EXC_NOMPPR "Partida",
			--CASE WHEN "ped"."OpenQty" = 0 THEN 'ATENDIDO' ELSE 'ABIERTO' END AS "Estado",
			CASE WHEN 
			"ped"."LineStatus" = 'C'
			THEN
				'CERRADO'
			ELSE
				'ABIERTO'
			END  "Estado",
			(SELECT	"CompnyName" FROM OADM) "Compania",
			(SELECT	"CompnyAddr" FROM OADM) "CompaniaDir",
			(SELECT	COALESCE("Phone1", '') || ' / ' || COALESCE("Phone2", '') FROM OADM)  "CompaniaTel",
			(SELECT	"E_Mail" FROM OADM) "CompaniaMail",
			(SELECT	"TaxIdNum" FROM OADM) "CompaniaRuc",
			COALESCE((SELECT STRING_AGG(T4."DocNum",',') 
				FROM 
		 		DLN1 T2 
				INNER JOIN ODLN T4 ON T4."DocEntry" = T2."DocEntry"
				WHERE T2."BaseEntry"  = "pec"."DocEntry" AND T2."BaseLine" = "ped"."LineNum"
				AND T2."BaseType" = 17
			),'') "NroEntrega",
			"suc"."BPLName",
			U_EXX_SCT_NOMBREUSRREG "UsuarioAutorizador",
			U_EXX_SCT_NOMBRETRABAJADOR "NombreTrabajador"
		FROM ORDR AS "pec" 
		INNER JOIN RDR1 AS "ped" ON "pec"."DocEntry" = "ped"."DocEntry"
		INNER JOIN OBPL "suc" ON "suc"."BPLId" = "pec"."BPLId"
		WHERE "ped"."DocEntry" = :P_DocEntry;
	
END;

CREATE PROCEDURE "EXX_SCT_PartidaPresupuestal_Listar"
(
	IN P_PROYECTO varchar(15),
	IN P_ETAPA varchar(15),
	IN P_EMPLEADO int,
	IN P_SUCURSAL int
)
AS
-- =================================================
-- Author:		Carlos Ubillus
-- Create date: 29/09/2023
-- Description:	Lista de partida presupuestal
-- =================================================
BEGIN
			
	SELECT	T0.U_EXC_CODIPROY,
			T1.U_EXC_CODPARPR, 
			--T1.U_EXC_CODPARPR || '-' || T1.U_EXC_DESPARPR "U_EXC_DESPARPR", 
			T1.U_EXC_DESPARPR "U_EXC_DESPARPR", 
			T1.U_EXC_DESPARPR "DESCRIPCION",
			T2."AcctCode" /* T1.U_EXC_CODCCONT */ "CtaContable",
			T1.U_EXC_CENCOSTO "CentroCosto",
			T1.U_EXC_GERENCIA	"Gerencia"
	FROM "@EXC_PRESGENE" T0 
	INNER JOIN "@EXC_PRESGEN1" T1 ON T0."Code" = T1."Code" 
	LEFT JOIN OACT T2 ON T2."Segment_0" = T1.U_EXC_CODCCONT
	WHERE T2."Postable" = 'Y'
	  AND T0.U_EXC_ESTADO = 'A'
	  AND COALESCE(T0.U_EXC_CODIPROY, '') = :P_PROYECTO
	  AND COALESCE(T0.U_EXC_ETAPA, '') = :P_ETAPA
	  AND COALESCE(T0.U_EXC_IDEMPRE, 0) = :P_SUCURSAL -- CAST((SELECT COALESCE(U_EXX_SUCURSAL, '0') FROM "@EXX_SCT_USER" EMP WHERE EMP."U_EXX_EMPLEADO" = :P_EMPLEADO) AS INT)--:P_SUCURSAL
	  AND (T1.U_EXC_CENCOSTO = (SELECT COALESCE(U_EXC_CECO, '') FROM OHEM WHERE "empID" = :P_EMPLEADO))-- OR 
	  	   --COALESCE((SELECT COALESCE(U_EXC_CECO, '') FROM OHEM WHERE "empID" = :P_EMPLEADO),'') = '')
	ORDER BY U_EXC_CODPARPR;

END;

CREATE PROCEDURE "EXX_SCT_PedidoVentaxVendedor_Listar"
(
	IN P_Usuario 	varchar(15),
	IN P_FecIni 	varchar(8),
	IN P_FecFin 	varchar(8),
	IN P_Cliente 	varchar(50),
	IN P_Estado 	varchar(1),
	IN P_Articulo	varchar(100) default ''
)
AS
BEGIN
	
	SELECT	DISTINCT
			"pec"."DocEntry",
			"pec"."DocNum" as "NroPed",
			"cli"."CardName" as "NombreCliente",
			"pec"."DocCur" as "Moneda",
			--"pec"."NumAtCard" as "NroOc",
			--"pec"."DiscPrcnt" as "Descuento",
			CASE WHEN "pec"."DocCur" = 'SOL' THEN "pec"."DocTotal" ELSE "pec"."DocTotalFC" END as "Total",
			"pec"."DocDate" as "FechaPed",
			"pec"."DocDueDate" as "FecEntrega",
			--'Creado' as "Estado",
			CASE WHEN "pec"."CANCELED" = 'Y' /* OR (COALESCE("pec".U_EXC_MOTANU, '') != '' AND "pec"."DocStatus" = 'C') */ THEN 'CANCELADO'
				 WHEN "pec"."DocStatus" = 'O' THEN 'ABIERTO'
				 WHEN "pec"."DocStatus" = 'C' THEN 'CERRADO'
			END  "Estado",
			"pec"."DocStatus",
			"suc"."BPLName",
			U_EXX_SCT_NOMBREUSRREG,
			U_EXX_SCT_NOMBRETRABAJADOR 
	FROM "ORDR" "pec"
	INNER JOIN "RDR1" "ped" ON "ped"."DocEntry" = "pec"."DocEntry"
	INNER JOIN "OCRD" "cli" ON "pec"."CardCode" = "cli"."CardCode"
	INNER JOIN "OSLP" "ven" ON "ven"."SlpCode" = "cli"."SlpCode"
	INNER JOIN OBPL "suc" ON "suc"."BPLId" = "pec"."BPLId"
	WHERE "pec"."U_EXX_SCT_USERREG" = 	:P_Usuario AND 
	("pec"."DocDate" >= :P_FecIni OR '0' = :P_FecIni)
	  AND ("pec"."DocDate" <= :P_FecFin OR '0' = :P_FecFin)
	  AND ("pec"."CardName" LIKE '%' || UPPER(:P_Cliente) || '%' OR
	       "pec"."CardCode" LIKE '%' || :P_Cliente || '%')
	  AND (("pec"."DocStatus" = :P_Estado AND "pec"."CANCELED" = 'N') OR
	  	   ("pec"."CANCELED" = 'Y' AND 'A' = :P_Estado) OR
	  	   '0' = :P_Estado)
	  AND ("ped"."ItemCode" LIKE '%' || :P_Articulo || '%' OR 
	  	   "ped"."Dscription" LIKE '%' || :P_Articulo || '%' OR 
	  	   '' = :P_Articulo)
	  AND U_EXC_CONSINTE = 'Y'
	ORDER BY "pec"."DocNum" DESC
	;
END;

CREATE PROCEDURE "EXX_SCT_Proyectos_Listar"
(
	IN P_Sucursal int
)
AS
BEGIN

	SELECT DISTINCT
	T0."U_EXC_CODIPROY" "PrjCode",
	T0."U_EXC_CODIPROY" || ' - ' || T0."U_EXC_NOMBPROY" AS "PrjName",
	T0."U_EXC_IDEMPRE"
	FROM "@EXC_PRESGENE" T0
	WHERE T0."U_EXC_ESTADO" = 'A'
	  AND T0."U_EXC_IDEMPRE" = :P_Sucursal
	ORDER BY T0."U_EXC_CODIPROY"
	;	
	
END;

CREATE PROCEDURE "EXX_SCT_SerieDocumento_Listar" 
(
	IN P_Tipo int,
	IN P_Sucursal int
)
AS
BEGIN
	
	SELECT	"Series", 
			"SeriesName"
	FROM NNM1
	WHERE "ObjectCode" = '17'
	  AND "Locked" = 'N'
	  AND IFNULL(U_EXC_CONSINTE, 'N') = 'Y'
	  AND "BPLId" = :P_Sucursal
	  AND :P_Tipo = 2

	UNION ALL

	SELECT	"Series", 
			"SeriesName"
	FROM NNM1
	WHERE "ObjectCode" = '1250000001' 
	  AND "Locked" = 'N'
	  AND IFNULL(U_EXC_CONSINTE, 'N') = 'Y'
	  AND "BPLId" = :P_Sucursal
	  AND :P_Tipo = 4
	  ;
END;
