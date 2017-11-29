RESTORE DATABASE M1_FM_DEV
FROM DISK = 'C:\Users\Andrii Diachenko\Documents\Visual Studio 2017\Projects\Freelance_DocumentAutomation\WpfAppTest\2017-09-18_DEV_M1_FM_DEV.bak'  
WITH 
MOVE 'M1_FM_DEV_Data' TO 'C:\Users\Andrii Diachenko\Documents\Visual Studio 2017\Projects\Freelance_DocumentAutomation\WpfAppTest\2017-09-18_DEV_M1_FM_DEV.mdf',
MOVE 'M1_FM_DEV_Log' TO 'C:\Users\Andrii Diachenko\Documents\Visual Studio 2017\Projects\Freelance_DocumentAutomation\WpfAppTest\2017-09-18_DEV_M1_FM_DEV.ldf'   
