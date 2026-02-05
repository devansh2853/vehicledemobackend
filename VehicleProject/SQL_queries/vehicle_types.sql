SET IDENTITY_INSERT VehicleTypes ON;

MERGE INTO VehicleTypes AS Target
USING (VALUES 
    (1, 'Car'),
    (2, 'Bus'),
    (3, 'Cycle')
) AS Source (Id, Name)
ON Target.Id = Source.Id

-- If the ID matches but the Name is different, update the Target
WHEN MATCHED AND Target.Name <> Source.Name THEN
    UPDATE SET Target.Name = Source.Name

-- If the ID doesn't exist at all, insert it
WHEN NOT MATCHED BY TARGET THEN
    INSERT (Id, Name) VALUES (Source.Id, Source.Name);

SET IDENTITY_INSERT VehicleTypes OFF;