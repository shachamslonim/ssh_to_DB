@echo on
setLocal EnableDelayedExpansion
title list_of_fapi_file 
IF not exist rpa_list2.txt goto needlist
cls
set I=
::get first errors
For /F "eol=# tokens=1,2,4 delims=; " %%I IN (rpa_list2.txt) DO (

IF not exist %%I mkdir %%I
IF exist %%I\ls.txt del %%I\ls.txt
plink.exe -pw %%K -ssh root@%%I "ls /home/kos/RPServers/RPServers_logs/fapi/fapi_server.log.*.gz| wc -l" >%%I\ls.txt	

) 
						




rem IF exist %%I\1.txt goto end
::PING 1.1.1.1 -n 1 -w 1000 >NUL

rem set v!N!=%%I 

::PING 1.1.1.1 -n 30 -w 1000 >NUL
sleep 10
goto END
:needlist
echo Missing file rpa_list2.txt
:END

echo done



