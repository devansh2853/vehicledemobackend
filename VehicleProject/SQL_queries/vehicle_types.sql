SET IDENTITY_INSERT VehicleTypes ON;

MERGE INTO VehicleTypes AS Target
USING (VALUES 
    (1, 'Car'),
    (2, 'Bus')
) AS Source (Id, Name)
ON Target.Id = Source.Id

-- If the ID matches but the Name is different, update the Target
WHEN MATCHED AND Target.Name <> Source.Name THEN
    UPDATE SET Target.Name = Source.Name

-- If the ID doesn't exist at all, insert it
WHEN NOT MATCHED BY TARGET THEN
    INSERT (Id, Name) VALUES (Source.Id, Source.Name)

-- If ID is in DB but not in script then delete it
WHEN NOT MATCHED BY SOURCE THEN
    DELETE;

SET IDENTITY_INSERT VehicleTypes OFF;