CREATE TABLE Files (
    Id SERIAL PRIMARY KEY,
    FileKey VARCHAR(255) NOT NULL,
    UserId INT NULL
);

ALTER TABLE "Users"
ADD COLUMN ProfilePictureFileId INT NULL;

ALTER TABLE "Users"
ADD CONSTRAINT FK_Users_Files FOREIGN KEY (ProfilePictureFileId) REFERENCES Files(Id);

ALTER TABLE "Loans"
ADD COLUMN LogoFileId INT NULL;

ALTER TABLE "Loans"
ADD CONSTRAINT FK_Loans_Files FOREIGN KEY (LogoFileId) REFERENCES Files(Id);

ALTER TABLE "AppliedLoans"
ALTER COLUMN "AppliedAmount" TYPE DECIMAL(18, 2) USING "AppliedAmount"::DECIMAL(18, 2),
ALTER COLUMN "AppliedRate" TYPE DECIMAL(5, 2) USING "AppliedRate"::DECIMAL(5, 2),
ALTER COLUMN "TermLength" TYPE INT USING "TermLength"::INT,
ALTER COLUMN "Status" TYPE INT USING "Status"::INT,
ALTER COLUMN "DateApplied" TYPE TIMESTAMP USING "DateApplied"::TIMESTAMP;


ALTER TABLE "Loans"
ADD COLUMN "MinLoanAmount" DECIMAL(18, 2),
ADD COLUMN "MaxLoanAmount" DECIMAL(18, 2);

UPDATE "Loans"
SET "MinLoanAmount" = CAST(SPLIT_PART(REPLACE(REPLACE("LoanAmount", '$', ''), ',', ''), '-', 1) AS DECIMAL),
    "MaxLoanAmount" = CAST(SPLIT_PART(REPLACE(REPLACE("LoanAmount", '$', ''), ',', ''), '-', 2) AS DECIMAL);

ALTER TABLE "Loans"
ADD COLUMN "MinInterestRate" DECIMAL(5, 2),
ADD COLUMN "MaxInterestRate" DECIMAL(5, 2);

UPDATE "Loans"
SET "MinInterestRate" = CAST(SPLIT_PART(REPLACE(SPLIT_PART("InterestRates", '%', 1), '-', ''), ' ', 1) AS DECIMAL),
    "MaxInterestRate" = CAST(SPLIT_PART(REPLACE(SPLIT_PART("InterestRates", '%', 2), ' ', ''), '-', 2) AS DECIMAL);

ALTER TABLE "Loans"
DROP COLUMN "LoanAmount",
DROP COLUMN "InterestRates";

ALTER TABLE "Loans"
ALTER COLUMN "MinCreditScore" TYPE INT USING "MinCreditScore"::INT,
ALTER COLUMN "TermLength" TYPE INT USING "TermLength"::INT,
ALTER COLUMN "ProcessingFee" TYPE DECIMAL(18, 2) USING "ProcessingFee"::DECIMAL(18, 2);

ALTER TABLE "Loans"
  ALTER COLUMN "Title" TYPE VARCHAR(40);

CREATE TYPE loan_status AS ENUM ('pending', 'approved', 'rejected');

ALTER TABLE "AppliedLoans"
ADD COLUMN "TempStatus" loan_status;

UPDATE "AppliedLoans"
SET "TempStatus" = CASE
    WHEN "Status" = 1 THEN 'pending'::loan_status
    WHEN "Status" = 2 THEN 'approved'::loan_status
    WHEN "Status" = 3 THEN 'rejected'::loan_status
END;

ALTER TABLE "AppliedLoans"
ALTER COLUMN "Status" TYPE loan_status USING "TempStatus";

ALTER TABLE "AppliedLoans"
DROP COLUMN "TempStatus";

ALTER TABLE "Users"
ADD COLUMN "FirstName" VARCHAR(50),
ADD COLUMN "LastName" VARCHAR(50);

UPDATE "Users"
SET
    "FirstName" = CASE
                    WHEN POSITION(' ' IN "Name") > 0 THEN SUBSTRING("Name" FROM 1 FOR POSITION(' ' IN "Name") - 1)
                    ELSE "Name"
                END,
    "LastName" = CASE
                    WHEN POSITION(' ' IN "Name") > 0 THEN SUBSTRING("Name" FROM POSITION(' ' IN "Name") + 1)
                    ELSE NULL -- Handle cases where there's no last name
                END;

ALTER TABLE "Users"
DROP COLUMN "Name";

CREATE TYPE role_enum AS ENUM ('CUSTOMER', 'MANAGER');
CREATE TYPE gender_enum AS ENUM ('male', 'female');

ALTER TABLE "Users"
    ALTER COLUMN "Role" TYPE role_enum USING ("Role"::role_enum),
    ALTER COLUMN "Gender" TYPE gender_enum USING ("Gender"::gender_enum);

UPDATE "Users"
SET "Role" = CASE
               WHEN "Role" = 'CUSTOMER' THEN 'CUSTOMER'::role_enum
               WHEN "Role" = 'MANAGER' THEN 'MANAGER'::role_enum
           END,
    "Gender" = CASE
                 WHEN "Gender" = 'male' THEN 'male'::gender_enum
                 WHEN "Gender" = 'female' THEN 'female'::gender_enum
             END;

CREATE EXTENSION IF NOT EXISTS pgcrypto;

ALTER TABLE "Users"
ADD COLUMN "Salt" VARCHAR(60);

ALTER TABLE "Users"
ALTER COLUMN "Password" TYPE VARCHAR(128) USING "Password";

--change all text to varchar fixed
--see what to do with images
--add all local paths of image to file table and add references

