select * from public."Loans"

INSERT INTO public."Loans" ("Logo", "Title", "LoanAmount", "InterestRates", "MinCreditScore", "TermLength", "ProcessingFee")
VALUES
    ('https://play-lh.googleusercontent.com/GdICLxnKd-e-lmV46N_SjihWHzAUX1nj8e--dF2KrQjvomN4zxOR-iEWwsTG_Tqr_oc', 'Bank of America', '$5,000-$100,000', '6.23%-20.23%', 450, 2, 120),
    ('https://download.logo.wine/logo/Chase_Bank/Chase_Bank-Logo.wine.png', 'Chase', '$5,000-$100,000', '4.99%-15.69%', 660, 4, 200),
    ('https://png.pngitem.com/pimgs/s/144-1449994_state-farm-samantha-alberson-png-download-state-farm.png', 'state Farm', '$5,000-$100,000', '5.78%-12.92%', 660, 5, 100);
