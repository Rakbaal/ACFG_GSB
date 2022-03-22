--Création de la base de donnee
CREATE DATABASE ACFG_LaboGSB
go
use ACFG_LaboGSB
go

--Création de la table VISITEUR
CREATE TABLE VISITEUR (
 VIS_ID INT IDENTITY (1,1),
 VIS_PRENOM VARCHAR(38) NOT NULL,
 VIS_NOM VARCHAR(38) NOT NULL,
 VIS_MDP VARCHAR(1000) NOT NULL,
 VIS_LOGIN CHAR(4) NOT NULL,
 CONSTRAINT PK_VISITEUR_ID PRIMARY KEY (VIS_ID)
)
go

--Création de la table MEDICAMENT
CREATE TABLE MEDICAMENT (
 MED_ID INT IDENTITY (1,1),
 MED_NOM_COMMERCIAL VARCHAR(38) NOT NULL,
 MED_NOM_DCI VARCHAR(38) NOT NULL,
 MED_DOSAGE VARCHAR(38) NOT NULL,
 MED_DESCRIPTION TEXT NOT NULL,
 MED_TYPE VARCHAR(38) NOT NULL,
 CONSTRAINT PK_MEDICAMENT_ID PRIMARY KEY (MED_ID)
 )
 go

--Création de la table PRATICIEN
CREATE TABLE PRATICIEN (
 PRA_ID INT IDENTITY (1,1),
 PRA_NOM VARCHAR(38) NOT NULL,
 PRA_PRENOM VARCHAR(38) NOT NULL,
 PRA_PROFESSION VARCHAR(38) NOT NULL,
 CONSTRAINT PK_PRATICIEN_ID PRIMARY KEY (PRA_ID)
 )
 go

--Création de la table AVIS
CREATE TABLE AVIS(
AVI_ID INT IDENTITY (1,1),
AVI_DATE DATE NOT NULL,
AVI_NOTE INT NOT NULL,
AVI_COMMENTAIRES TEXT NOT NULL,
MED_ID INT NOT NULL,
CONSTRAINT PK_AVIS_ID PRIMARY KEY (AVI_ID),
CONSTRAINT FK_AVIS_MEDICAMENT FOREIGN KEY (MED_ID) REFERENCES MEDICAMENT (MED_ID)
)
go

--Création de la table ECRIRE
CREATE TABLE ECRIRE(
AVI_ID INT NOT NULL,
PRA_ID INT NOT NULL,
CONSTRAINT PK_ECRIRE_ID PRIMARY KEY (AVI_ID,PRA_ID),
CONSTRAINT FK_ECRIRE_AVIS FOREIGN KEY (AVI_ID) REFERENCES AVIS (AVI_ID),
CONSTRAINT FK_ECRIRE_PRATICIEN FOREIGN KEY (PRA_ID) REFERENCES PRATICIEN (PRA_ID)
)
go

--Création du trigger pour Hasher le mdp en SHA512 en base
CREATE TRIGGER TRI_HASHAGE
ON VISITEUR
INSTEAD OF INSERT
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @MDPHASH VARCHAR(1000)
	
	SELECT @MDPHASH = CONVERT(NVARCHAR(512),HashBytes('SHA2_512', VIS_MDP),2) FROM inserted 

	  INSERT dbo.VISITEUR(VIS_PRENOM, VIS_NOM, VIS_LOGIN, VIS_MDP)
		SELECT VIS_PRENOM, VIS_NOM, VIS_LOGIN, @MDPHASH
		FROM inserted
END
go

--Insertion Table VISITEUR
INSERT INTO VISITEUR (VIS_PRENOM, VIS_NOM, VIS_MDP, VIS_LOGIN)
VALUES ('Sebastien', 'AUBERT', 'sebaub273', 'sAUB')
go

--Insertion Table MEDICAMENT
INSERT INTO MEDICAMENT (MED_NOM_COMMERCIAL, MED_NOM_DCI, MED_DOSAGE, MED_DESCRIPTION, MED_TYPE)
VALUES ('Rimifon', 'Isoniazide', '100 mg', 'C''est un antibiotique utilisé en première intention pour la prévention et le traitement de la tuberculose latente et de la tuberculose active.', 'Comprimé')
INSERT INTO MEDICAMENT (MED_NOM_COMMERCIAL, MED_NOM_DCI, MED_DOSAGE, MED_DESCRIPTION, MED_TYPE)
VALUES ('Trizivir', 'Abacavir', '5 ml', 'C''est un inhibiteur nucléosidique très puissant de la transcriptase inverse, pour le traitement de l''infection au VIH', 'Sirop')
INSERT INTO MEDICAMENT (MED_NOM_COMMERCIAL, MED_NOM_DCI, MED_DOSAGE, MED_DESCRIPTION, MED_TYPE)
VALUES ('viread', 'Ténofovir', '300 mg', 'C''est un médicament anti-VIH appartenant à la classe des analogues nucléotidiques. Il est utilisé en association avec d''autres médicaments pour le traitement des personnes vivant avec le VIH.', 'Gélule')
INSERT INTO MEDICAMENT (MED_NOM_COMMERCIAL, MED_NOM_DCI, MED_DOSAGE, MED_DESCRIPTION, MED_TYPE)
VALUES ('Zovirax', 'Aciclovir', '250 mg', 'Ce médicament est un antiviral puissant, actif sur les virus du groupe de l''herpès. Il empêche la reproduction des virus dans les cellules infectées, mais ne peut détruire les virus cachés dans les ganglions nerveux, responsables des récurrences (récidives) qu''il ne peut éviter.', 'Injectable')
INSERT INTO MEDICAMENT (MED_NOM_COMMERCIAL, MED_NOM_DCI, MED_DOSAGE, MED_DESCRIPTION, MED_TYPE)
VALUES ('Flagyl', 'Métronidazole', '125 mg', 'C''est un antibiotique et antiparasitaire appartenant aux nitroimidazoles. Il inhibe la synthèse des acides nucléiques et est utilisé pour le traitement des infections liées à des bactéries anaérobies ainsi qu''à des protozoaires.', 'Suspension')
INSERT INTO MEDICAMENT (MED_NOM_COMMERCIAL, MED_NOM_DCI, MED_DOSAGE, MED_DESCRIPTION, MED_TYPE)
VALUES ('Darkatin', 'Miconazole', '2%', 'C''est un antimycosique imidazolé utilisé fréquemment dans des sprays topiques, des crèmes et lotions appliquées sur la peau pour guérir les infections fongiques tels le pied d''athlète et l''intertrigo inguinal. Il peut aussi servir en usage interne pour traiter les infections vaginales dues à des levures.', 'Crème')
INSERT INTO MEDICAMENT (MED_NOM_COMMERCIAL, MED_NOM_DCI, MED_DOSAGE, MED_DESCRIPTION, MED_TYPE)
VALUES ('SIGMA-ALDRICH', 'Fluoresceine', '1%', 'C''est une substance chimique complexe composée de deux molécules de phénols liées à un cycle pyrane lui-même relié à un acide benzoïque.', 'collyre')
INSERT INTO MEDICAMENT (MED_NOM_COMMERCIAL, MED_NOM_DCI, MED_DOSAGE, MED_DESCRIPTION, MED_TYPE)
VALUES ('Cortisone', 'Hydrocortisone', '1%', 'C''est le nom de l''hormone cortisol lorsqu''elle est fournie comme médicament. Les utilisations comprennent des affections telles que l''insuffisance surrénocorticale, le syndrome surrénogénital, l''hypocalcémie, la thyroïdite, la polyarthrite rhumatoïde, la dermatite, l''asthme et la BPCO', 'Pommade')
go

--insertion Table Praticien
INSERT INTO PRATICIEN (PRA_NOM,PRA_PRENOM,PRA_PROFESSION)
VALUES ('Delamare', 'Paul', 'Chirurgien cardiaque')
INSERT INTO PRATICIEN (PRA_NOM,PRA_PRENOM,PRA_PROFESSION)
VALUES ('Roost', 'Didier', 'Pharmacien')
INSERT INTO PRATICIEN (PRA_NOM,PRA_PRENOM,PRA_PROFESSION)
VALUES ('Plaza', 'Stephane', 'Infirmier')
INSERT INTO PRATICIEN (PRA_NOM,PRA_PRENOM,PRA_PROFESSION)
VALUES ('Deschamps', 'Marie-Jeanne', 'Gynécologue')
INSERT INTO PRATICIEN (PRA_NOM,PRA_PRENOM,PRA_PROFESSION)
VALUES ('Lagrosse', 'Bertha', 'Infirmière')

--Création de la procedure Login Validation
CREATE PROC PS_LOGIN_VALIDATION
	@Login CHAR(4),
	@Mdp VARCHAR(512)
AS
	SELECT VIS_ID
	FROM VISITEUR
	WHERE VIS_LOGIN = @Login
	AND VIS_MDP = @Mdp
go

-- Création de la procédure stockée CREATE des CRUD
CREATE PROC PS_CREATE_MEDICAMENT
	@NomCommercial VARCHAR(38),
	@NomDCI VARCHAR(38),
	@Dosage VARCHAR(38),
	@Description TEXT,
	@Type VARCHAR(38)
AS
	SET ROWCOUNT 0
	IF exists(SELECT MED_NOM_COMMERCIAL FROM MEDICAMENT WHERE MED_NOM_COMMERCIAL = @NomCommercial)
		begin
			-- Annule l'insertion si le libellé existe déjà
			-- Renvoie un Code 1 pour "Exécution arrêtée"
			SELECT 1 as 'stateMessage'
		end
	ELSE
		begin
			-- Effectue l'insertion si le libellé n'existe pas
			INSERT INTO MEDICAMENT(MED_NOM_COMMERCIAL, MED_NOM_DCI, MED_DOSAGE, MED_DESCRIPTION, MED_TYPE) 
				VALUES(@NomCommercial, @NomDCI, @Dosage, @Description, @Type)
			-- Renvoie un Code 0 pour "Exécution réussie"
			SELECT 0 as 'stateMessage'
		end
go


-- Création de la procédure stockée UPDATE des CRUD
CREATE PROC PS_UPDATE_MEDICAMENT
	@id int,
	@NomCommercial VARCHAR(38),
	@NomDCI VARCHAR(38),
	@Dosage VARCHAR(38),
	@Description TEXT,
	@Type VARCHAR(38)
AS
	declare @placeHolder VARCHAR(38)
	BEGIN
		UPDATE MEDICAMENT
		SET MED_NOM_COMMERCIAL = @NomCommercial, MED_NOM_DCI = @NomDCI, MED_DOSAGE = @Dosage, MED_DESCRIPTION = @Description, MED_TYPE = @Type
		WHERE MED_ID = @id
	END
go

-- Création de la procédure stockée de SELECT de tous les médicaments SANS description
CREATE PROC PS_SELECT_ALL_MEDICAMENT
AS
	SELECT MED_ID, MED_NOM_COMMERCIAL, MED_NOM_DCI, MED_DOSAGE, MED_TYPE
	FROM MEDICAMENT
go

-- Création de la procédure stockée de SELECT du médicament concerné AVEC description
CREATE PROC PS_SELECT_MEDICAMENT_DESCRIPTION
	@IdMedicament INT
AS
	IF exists(SELECT MED_ID FROM MEDICAMENT WHERE MED_ID = @IdMedicament)
		begin
			SELECT MED_ID, MED_NOM_COMMERCIAL, MED_NOM_DCI, MED_DOSAGE, MED_TYPE, MED_DESCRIPTION
			FROM MEDICAMENT
			WHERE MED_ID = @IdMedicament
		end
go

-- Suppression d'un médicaments
CREATE PROC PS_DELETE_MEDICAMENT
	@IdMedicament INT
AS
	IF exists(SELECT MED_ID FROM MEDICAMENT WHERE MED_ID = @IdMedicament)
		begin
			DELETE FROM MEDICAMENT 
			WHERE MED_ID = @IdMedicament
		end
go