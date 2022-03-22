--Cr�ation de la base de donnee
CREATE DATABASE ACFG_LaboGSB
go
use ACFG_LaboGSB
go

--Cr�ation de la table VISITEUR
CREATE TABLE VISITEUR (
 VIS_ID INT IDENTITY (1,1),
 VIS_PRENOM VARCHAR(38) NOT NULL,
 VIS_NOM VARCHAR(38) NOT NULL,
 VIS_MDP VARCHAR(1000) NOT NULL,
 VIS_LOGIN CHAR(4) NOT NULL,
 CONSTRAINT PK_VISITEUR_ID PRIMARY KEY (VIS_ID)
)
go

--Cr�ation de la table MEDICAMENT
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

--Cr�ation de la table PRATICIEN
CREATE TABLE PRATICIEN (
 PRA_ID INT IDENTITY (1,1),
 PRA_NOM VARCHAR(38) NOT NULL,
 PRA_PRENOM VARCHAR(38) NOT NULL,
 PRA_PROFESSION VARCHAR(38) NOT NULL,
 CONSTRAINT PK_PRATICIEN_ID PRIMARY KEY (PRA_ID)
 )
 go

--Cr�ation de la table AVIS
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

--Cr�ation de la table ECRIRE
CREATE TABLE ECRIRE(
AVI_ID INT NOT NULL,
PRA_ID INT NOT NULL,
CONSTRAINT PK_ECRIRE_ID PRIMARY KEY (AVI_ID,PRA_ID),
CONSTRAINT FK_ECRIRE_AVIS FOREIGN KEY (AVI_ID) REFERENCES AVIS (AVI_ID),
CONSTRAINT FK_ECRIRE_PRATICIEN FOREIGN KEY (PRA_ID) REFERENCES PRATICIEN (PRA_ID)
)
go

--Cr�ation du trigger pour Hasher le mdp en SHA512 en base
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
VALUES ('Rimifon', 'Isoniazide', '100 mg', 'C''est un antibiotique utilis� en premi�re intention pour la pr�vention et le traitement de la tuberculose latente et de la tuberculose active.', 'Comprim�')
INSERT INTO MEDICAMENT (MED_NOM_COMMERCIAL, MED_NOM_DCI, MED_DOSAGE, MED_DESCRIPTION, MED_TYPE)
VALUES ('Trizivir', 'Abacavir', '5 ml', 'C''est un inhibiteur nucl�osidique tr�s puissant de la transcriptase inverse, pour le traitement de l''infection au VIH', 'Sirop')
INSERT INTO MEDICAMENT (MED_NOM_COMMERCIAL, MED_NOM_DCI, MED_DOSAGE, MED_DESCRIPTION, MED_TYPE)
VALUES ('viread', 'T�nofovir', '300 mg', 'C''est un m�dicament anti-VIH appartenant � la classe des analogues nucl�otidiques. Il est utilis� en association avec d''autres m�dicaments pour le traitement des personnes vivant avec le VIH.', 'G�lule')
INSERT INTO MEDICAMENT (MED_NOM_COMMERCIAL, MED_NOM_DCI, MED_DOSAGE, MED_DESCRIPTION, MED_TYPE)
VALUES ('Zovirax', 'Aciclovir', '250 mg', 'Ce m�dicament est un antiviral puissant, actif sur les virus du groupe de l''herp�s. Il emp�che la reproduction des virus dans les cellules infect�es, mais ne peut d�truire les virus cach�s dans les ganglions nerveux, responsables des r�currences (r�cidives) qu''il ne peut �viter.', 'Injectable')
INSERT INTO MEDICAMENT (MED_NOM_COMMERCIAL, MED_NOM_DCI, MED_DOSAGE, MED_DESCRIPTION, MED_TYPE)
VALUES ('Flagyl', 'M�tronidazole', '125 mg', 'C''est un antibiotique et antiparasitaire appartenant aux nitroimidazoles. Il inhibe la synth�se des acides nucl�iques et est utilis� pour le traitement des infections li�es � des bact�ries ana�robies ainsi qu''� des protozoaires.', 'Suspension')
INSERT INTO MEDICAMENT (MED_NOM_COMMERCIAL, MED_NOM_DCI, MED_DOSAGE, MED_DESCRIPTION, MED_TYPE)
VALUES ('Darkatin', 'Miconazole', '2%', 'C''est un antimycosique imidazol� utilis� fr�quemment dans des sprays topiques, des cr�mes et lotions appliqu�es sur la peau pour gu�rir les infections fongiques tels le pied d''athl�te et l''intertrigo inguinal. Il peut aussi servir en usage interne pour traiter les infections vaginales dues � des levures.', 'Cr�me')
INSERT INTO MEDICAMENT (MED_NOM_COMMERCIAL, MED_NOM_DCI, MED_DOSAGE, MED_DESCRIPTION, MED_TYPE)
VALUES ('SIGMA-ALDRICH', 'Fluoresceine', '1%', 'C''est une substance chimique complexe compos�e de deux mol�cules de ph�nols li�es � un cycle pyrane lui-m�me reli� � un acide benzo�que.', 'collyre')
INSERT INTO MEDICAMENT (MED_NOM_COMMERCIAL, MED_NOM_DCI, MED_DOSAGE, MED_DESCRIPTION, MED_TYPE)
VALUES ('Cortisone', 'Hydrocortisone', '1%', 'C''est le nom de l''hormone cortisol lorsqu''elle est fournie comme m�dicament. Les utilisations comprennent des affections telles que l''insuffisance surr�nocorticale, le syndrome surr�nog�nital, l''hypocalc�mie, la thyro�dite, la polyarthrite rhumato�de, la dermatite, l''asthme et la BPCO', 'Pommade')
go

--insertion Table Praticien
INSERT INTO PRATICIEN (PRA_NOM,PRA_PRENOM,PRA_PROFESSION)
VALUES ('Delamare', 'Paul', 'Chirurgien cardiaque')
INSERT INTO PRATICIEN (PRA_NOM,PRA_PRENOM,PRA_PROFESSION)
VALUES ('Roost', 'Didier', 'Pharmacien')
INSERT INTO PRATICIEN (PRA_NOM,PRA_PRENOM,PRA_PROFESSION)
VALUES ('Plaza', 'Stephane', 'Infirmier')
INSERT INTO PRATICIEN (PRA_NOM,PRA_PRENOM,PRA_PROFESSION)
VALUES ('Deschamps', 'Marie-Jeanne', 'Gyn�cologue')
INSERT INTO PRATICIEN (PRA_NOM,PRA_PRENOM,PRA_PROFESSION)
VALUES ('Lagrosse', 'Bertha', 'Infirmi�re')

--Cr�ation de la procedure Login Validation
CREATE PROC PS_LOGIN_VALIDATION
	@Login CHAR(4),
	@Mdp VARCHAR(512)
AS
	SELECT VIS_ID
	FROM VISITEUR
	WHERE VIS_LOGIN = @Login
	AND VIS_MDP = @Mdp
go

-- Cr�ation de la proc�dure stock�e CREATE des CRUD
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
			-- Annule l'insertion si le libell� existe d�j�
			-- Renvoie un Code 1 pour "Ex�cution arr�t�e"
			SELECT 1 as 'stateMessage'
		end
	ELSE
		begin
			-- Effectue l'insertion si le libell� n'existe pas
			INSERT INTO MEDICAMENT(MED_NOM_COMMERCIAL, MED_NOM_DCI, MED_DOSAGE, MED_DESCRIPTION, MED_TYPE) 
				VALUES(@NomCommercial, @NomDCI, @Dosage, @Description, @Type)
			-- Renvoie un Code 0 pour "Ex�cution r�ussie"
			SELECT 0 as 'stateMessage'
		end
go


-- Cr�ation de la proc�dure stock�e UPDATE des CRUD
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

-- Cr�ation de la proc�dure stock�e de SELECT de tous les m�dicaments SANS description
CREATE PROC PS_SELECT_ALL_MEDICAMENT
AS
	SELECT MED_ID, MED_NOM_COMMERCIAL, MED_NOM_DCI, MED_DOSAGE, MED_TYPE
	FROM MEDICAMENT
go

-- Cr�ation de la proc�dure stock�e de SELECT du m�dicament concern� AVEC description
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

-- Suppression d'un m�dicaments
CREATE PROC PS_DELETE_MEDICAMENT
	@IdMedicament INT
AS
	IF exists(SELECT MED_ID FROM MEDICAMENT WHERE MED_ID = @IdMedicament)
		begin
			DELETE FROM MEDICAMENT 
			WHERE MED_ID = @IdMedicament
		end
go