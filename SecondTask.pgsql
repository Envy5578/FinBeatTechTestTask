	SELECT 
    c.ClientName,
    (
        SELECT COUNT(*) 
        FROM ClientContacts cc 
        WHERE cc.ClientId = c.Id
    ) AS ContactCount
FROM 
    Clients c;