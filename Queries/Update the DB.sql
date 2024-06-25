CREATE TABLE Files (
    Id SERIAL PRIMARY KEY,
    FilePath VARCHAR(255) NOT NULL
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

CREATE TYPE "Status" AS ENUM ('pending', 'approved', 'rejected');

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

-- RUN Password Hasher in the solution
CREATE EXTENSION IF NOT EXISTS pgcrypto;

ALTER TABLE "Users"
ADD COLUMN "Salt" VARCHAR(60);

ALTER TABLE "Users"
ALTER COLUMN "Password" TYPE VARCHAR(128) USING "Password";

ALTER TABLE "Users"
ALTER COLUMN "Email" TYPE VARCHAR(255) USING "Email";

ALTER TABLE "Users"
ALTER COLUMN "Employer" TYPE VARCHAR(40) USING "Employer";

ALTER TABLE "Users"
ALTER COLUMN "Designation" TYPE VARCHAR(40) USING "Designation";

-- Run image downloader in the solution

ALTER TABLE "Loans"
DROP COLUMN "Logo";

ALTER TABLE "Users"
DROP COLUMN "UserPic";
--update logic to file upload

ALTER TABLE "Loans" ALTER COLUMN "TermLength" TYPE numeric USING "TermLength"::numeric;
ALTER TYPE gender_enum RENAME TO "Gender";
ALTER TYPE role_enum RENAME TO "Role";
ALTER TYPE loan_status RENAME TO "Status";

select n.nspname as enum_schema,  
       t.typname as enum_name,  
       e.enumlabel as enum_value
from pg_type t 
   join pg_enum e on t.oid = e.enumtypid  
   join pg_catalog.pg_namespace n ON n.oid = t.typnamespace;

ALTER TABLE files RENAME TO "Files";

ALTER TABLE "Files" RENAME COLUMN id TO "Id";
ALTER TABLE "Files" RENAME COLUMN "Filepath" TO "FilePath";

ALTER TABLE "Loans" RENAME COLUMN logofileid TO "LogoFileId";