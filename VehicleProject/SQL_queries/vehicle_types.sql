IF NOT EXISTS (SELECT 1 FROM VehicleTypes WHERE Id = 1) INSERT INTO VehicleTypes (Id, Name) VALUES (1, 'Car');
IF NOT EXISTS (SELECT 1 FROM VehicleTypes WHERE Id = 2) INSERT INTO VehicleTypes (Id, Name) VALUES (2, 'Bus');
   