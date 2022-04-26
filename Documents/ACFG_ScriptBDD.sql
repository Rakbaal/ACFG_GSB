use master
go

drop database ACFG_LaboGSB
go
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
CREATE TABLE AVIS (
AVI_ID INT IDENTITY (1,1),
AVI_DATE DATE NOT NULL,
AVI_COMMENTAIRE TEXT NOT NULL,
MED_ID INT NOT NULL,
PRA_ID INT NOT NULL,
CONSTRAINT PK_AVIS_ID PRIMARY KEY (AVI_ID),
CONSTRAINT FK_AVIS_MEDICAMENT FOREIGN KEY (MED_ID) REFERENCES MEDICAMENT (MED_ID),
CONSTRAINT FK_AVIS_PRATICIEN FOREIGN KEY (PRA_ID) REFERENCES PRATICIEN (PRA_ID)
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
go

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


-- MEDICAMENTS

		-- Cr�ation de m�dicament
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

		-- Lecture des m�dicaments (sans description)
		CREATE PROC PS_SELECT_ALL_MEDICAMENT
		AS
			SELECT MED_ID, MED_NOM_COMMERCIAL, MED_NOM_DCI, MED_DOSAGE, MED_TYPE
			FROM MEDICAMENT
		go


		-- Lecture des m�dicaments (avec description)
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


		-- MAJ des m�dicaments
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

		

		-- Suppression des m�dicaments
		CREATE PROC PS_DELETE_MEDICAMENT
			@IdMedicament INT
		AS
			IF exists(SELECT MED_ID FROM MEDICAMENT WHERE MED_ID = @IdMedicament)
				begin
					DELETE FROM MEDICAMENT 
					WHERE MED_ID = @IdMedicament
				end
		go

-- PRATICIEN
		
		-- S�lection d'un praticien
		CREATE PROC PS_SELECT_UNPRATICIEN
				@ID int
			AS
				SELECT PRA_ID, PRA_NOM, PRA_PRENOM, PRA_PROFESSION
				FROM PRATICIEN
				WHERE PRA_ID = @ID
		go

		-- Cr�ation de Praticien
		CREATE PROC PS_CREATE_PRATICIEN
			@Nom VARCHAR(38),
			@Prenom VARCHAR(38),
			@Profession VARCHAR(38)
		AS
			SET ROWCOUNT 0
			IF exists(SELECT PRA_NOM, PRA_PRENOM FROM PRATICIEN WHERE PRA_NOM = @Nom AND PRA_PRENOM = @Prenom)
				begin
					-- Annule l'insertion si le libell� existe d�j�
					-- Renvoie un Code 1 pour "Ex�cution arr�t�e"
					SELECT 1 as 'stateMessage'
				end
			ELSE
				begin
					-- Effectue l'insertion si le libell� n'existe pas
					INSERT INTO PRATICIEN(PRA_NOM, PRA_PRENOM, PRA_PROFESSION) 
						VALUES(@Nom, @Prenom, @Profession)
					-- Renvoie un Code 0 pour "Ex�cution r�ussie"
					SELECT 0 as 'stateMessage'
				end
		go

		-- Lecture des praticiens
		CREATE PROC PS_SELECT_ALL_PRATICIEN
		AS
			SELECT PRA_ID, PRA_NOM, PRA_PRENOM, PRA_PROFESSION
			FROM PRATICIEN
		go

		-- MAJ des praticiens
		CREATE PROC PS_UPDATE_PRATICIEN
			@id int,
			@Nom VARCHAR(38),
			@Prenom VARCHAR(38),
			@Profession VARCHAR(38)
		AS
			BEGIN
				UPDATE PRATICIEN
				SET PRA_NOM = @Nom, PRA_PRENOM = @Prenom, PRA_PROFESSION = @Profession
				WHERE PRA_ID = @id
			END
		go

		-- Suppression des praticiens
		CREATE PROC PS_DELETE_PRATICIEN
			@IdPraticien INT 
		AS
			IF exists(SELECT PRA_ID FROM PRATICIEN WHERE PRA_ID = @IdPraticien)
				begin
					DELETE FROM PRATICIEN 
					WHERE PRA_ID = @IdPraticien
				end
		go

-- AVIS

		-- Cr�ation d'un avis
		CREATE PROC PS_CREATE_AVIS
			@IdMedicament INT,
			@date DATE,
			@Commentaires TEXT,
			@idPraticien INT

		AS
			IF exists(SELECT MED_ID FROM AVIS WHERE MED_ID = @IdMedicament)
				begin
					SELECT 1 as 'stateMessage'
				end
			ELSE
				begin
					INSERT INTO AVIS(AVI_DATE, AVI_COMMENTAIRE, PRA_ID, MED_ID) 
						VALUES(@date, @Commentaires, @idPraticien, @IdMedicament)
					SELECT 0 as 'stateMessage'
				end
		go

		-- Lecture de la liste des avis pour un m�dicament pr�cis
		CREATE PROC PS_SELECT_AVIS_MEDICAMENT
			@IdMedicament INT
		AS
			IF exists(SELECT MED_ID FROM MEDICAMENT WHERE MED_ID = @IdMedicament)
				begin
					SELECT AVI_ID, AVI_DATE, AVI_COMMENTAIRE, A.PRA_ID
					FROM AVIS AS A
					INNER JOIN MEDICAMENT AS M ON A.MED_ID = M.MED_ID
					INNER JOIN PRATICIEN AS P ON P.PRA_ID = A.PRA_ID
					WHERE A.MED_ID = @IdMedicament
				end
		go

		-- Supprimer un avis

		CREATE PROC PS_DELETE_AVIS
			@IdAvis INT
		AS
			IF exists(SELECT AVI_ID FROM AVIS WHERE AVI_ID = @IdAvis)
				begin
					DELETE FROM AVIS
					WHERE AVI_ID = @IdAvis
				end
		go
