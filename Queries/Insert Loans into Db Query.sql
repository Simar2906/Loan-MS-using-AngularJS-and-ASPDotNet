select * from public."Loans"

INSERT INTO public."Loans" ("Logo", "Title", "LoanAmount", "InterestRates", "MinCreditScore", "TermLength", "ProcessingFee")
VALUES
    ('https://play-lh.googleusercontent.com/GdICLxnKd-e-lmV46N_SjihWHzAUX1nj8e--dF2KrQjvomN4zxOR-iEWwsTG_Tqr_oc', 'Bank of America', '$5,000-$100,000', '6.23%-20.23%', 450, 2, 120),
    ('https://download.logo.wine/logo/Chase_Bank/Chase_Bank-Logo.wine.png', 'Chase', '$5,000-$100,000', '4.99%-15.69%', 660, 4, 200),
    ('https://png.pngitem.com/pimgs/s/144-1449994_state-farm-samantha-alberson-png-download-state-farm.png', 'State Farm', '$5,000-$100,000', '5.78%-12.92%', 660, 5, 100);

delete from "Loans"
select * from public."__EFMigrationsHistory"
select * from "Loans"

INSERT INTO "AppliedLoans" ("UserId", "AppliedAmount", "AppliedRate", "TermLength", "Status", "DateApplied", "LoanId")
VALUES
(1, 12345, 12, 6, 3, '2024-01-05T10:00:09.818Z', 3),
(1, 50000, 15, 12, 1, '2024-01-05T10:24:13.020Z', 1),
(1, 5900, 12, 5, 1, '2024-01-08T04:52:49.241Z', 2),
(3, 100000, 15, 12, 1, '2024-01-08T06:52:34.320Z', 1),
(5, 15426, 11, 32, 1, '2024-01-08T06:57:54.024Z', 3),
(5, 23412, 12, 23, 3, '2024-01-08T07:15:27.581Z', 2),
(1, 12341, 12, 12, 1, '2024-01-08T07:30:01.558Z', 2),
(1, 23145, 12.3, 23, 1, '2024-01-09T04:00:15.346Z', 3),
(5, 25000, 12, 12, 1, '2024-01-12T09:18:33.360Z', 2),
(1, 12412, 12, 12, 3, '2024-01-12T09:28:52.156Z', 1),
(1, 12456, 7, 33, 1, '2024-01-12T09:38:48.693Z', 3),
(3, 42324, 12, 12, 3, '2024-01-12T12:23:35.472Z', 3),
(5, 10000, 12, 40, 1, '2024-01-12T12:42:18.993Z', 3),
(5, 50000, 12, 20, 1, '2024-01-14T15:38:58.743Z', 3),
(5, 10000, 10, 20.8, 3, '2024-01-14T15:43:03.580Z', 1);

SELECT al."Id", u."Email", u."Gender", u."Name", u."Password", u."Role", u."UserPic", l."Logo", l."Title", 
l."LoanAmount", l."InterestRates", al."AppliedAmount", al."AppliedRate", l."MinCreditScore", al."TermLength", 
l."ProcessingFee",u."Employer", u."Salary", u."Designation", al."Status", al."DateApplied" FROM public."AppliedLoans" 
al LEFT JOIN public."Users" u ON u."Id"=al."UserId" LEFT JOIN public."Loans" l ON l."Id"=al."LoanId" WHERE al."UserId"=1