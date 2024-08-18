@echo on
setLocal EnableDelayedExpansion
title Core watchdog 
IF not exist rpa_list2.txt goto needlist
cls
set I=
::get first errors
For /F "eol=# tokens=1,2,4 delims=; " %%I IN (rpa_list2.txt) DO (

 


 IF not exist %%I mkdir %%I
 IF exist %%I\all_logs_old.txt del %%I\all_logs_old.txt
 IF exist %%I\catalina_old.txt del %%I\catalina_old.txt
 IF exist %%I\control_old.txt del %%I\control_old.txt
 IF exist %%I\fapi_server_old.txt del %%I\fapi_server_old.txt
 IF exist %%I\management_old.txt del %%I\management_old.txt
 IF exist %%I\replication_old.txt del %%I\replication_old.txt
 IF exist %%I\tomcat_old.txt del %%I\tomcat_old.txt
 IF exist %%I\1.txt del %%I\1.txt


 IF exist %%I\all_logs.txt move %%I\all_logs.txt %%I\all_logs_old.txt
 IF exist %%I\catalina.txt move %%I\catalina.txt %%I\catalina_old.txt
 IF exist %%I\control.txt move %%I\control.txt %%I\control_old.txt
 IF exist %%I\fapi_server.txt move %%I\fapi_server.txt %%I\fapi_server_old.txt
 IF exist %%I\management.txt move %%I\management.txt %%I\management_old.txt
 IF exist %%I\replication.txt move %%I\replication.txt %%I\replication_old.txt
 IF exist %%I\tomcat.txt move %%I\tomcat.txt %%I\tomcat_old.txt
 IF exist %%I\HERE.txt move %%I\HERE.txt %%I\HERE_old.txt

)
echo.
echo.
echo ++++++++++++++ End of display errors in /home/kos/installationLogs/server.log ++++++++++++++++++++
echo.
echo.
::PING 1.1.1.1 -n 5 -w 1000 >NUL
rem sleep 1 secend
ping 1.1.1.1 -n 1 -w 1000 > nul 

cls
	For /F "eol=# tokens=1,2,4 delims=; " %%I IN (rpa_list2.txt) DO (

	if %%J == ESX (

					echo /home/kos/Kdriver >%%I\HERE.txt
					plink.exe -pw %%K -ssh root@%%I grep -E '"Assertion|error|HERE|DEVICE_STATE_SUSPECTED|fatal|deadlock"' /scratch/log/kdriver.log.* >> %%I\HERE.txt
					 ) else (
	plink.exe -pw %%K -ssh root@%%I "find / -type f -size +8000k -exec ls -lh {} \; | grep core\\. ">>%%I\1.txt
	::IF ERRORLEVEL 1 GOTO End
    plink.exe -pw %%K -ssh root@%%I grep ERROR /home/kos/installationLogs/server.log >%%I\servererror.txt
	echo  ********************/home/kos/replication/result.log.00********************************* >%%I\replication.txt
	plink.exe -pw %%K -ssh root@%%I grep HERE /home/kos/replication/result.log.00 >>%%I\replication.txt
	plink.exe -pw %%K -ssh root@%%I grep Assertion /home/kos/replication/result.log.00 >>%%I\replication.txt
	plink.exe -pw %%K -ssh root@%%I grep 'DLManager: deadlock suspected' /home/kos/replication/result.log.00 >>%%I\replication.txt
	echo *********************/home/kos/management/result.log.00********************************** >>%%I\management.txt
	plink.exe -pw %%K -ssh root@%%I grep HERE /home/kos/management/result.log.00 >>%%I\management.txt
	plink.exe -pw %%K -ssh root@%%I grep Assertion /home/kos/management/result.log.00 >>%%I\management.txt
	echo ********************/home/kos/control/result.log.00************************************** >>%%I\control.txt
	plink.exe -pw %%K -ssh root@%%I grep HERE /home/kos/control/result.log.00 >>%%I\control.txt
	plink.exe -pw %%K -ssh root@%%I grep Assertion /home/kos/control/result.log.00 >>%%I\control.txt
	plink.exe -pw %%K -ssh root@%%I grep OCW /home/kos/control/result.log.00 >>%%I\control.txt		


	echo  ********************/home/kos/replication/result.log%%C%%D********************************* >>%%I\replication.txt
	plink.exe -pw %%K -ssh root@%%I zgrep -E '"HERE|Assertion|DLManager: deadlock suspected"' /home/kos/replication/*.gz  >>%%I\replication.txt

	echo *********************/home/kos/management/result.log%%C%%D********************************** >>%%I\management.txt
	plink.exe -pw %%K -ssh root@%%I zgrep -E '"HERE|Assertion"' /home/kos/management/*.gz >>%%I\management.txt

	echo ********************/home/kos/control/result.log%%C%%D************************************** >>%%I\control.txt
	plink.exe -pw %%K -ssh root@%%I zgrep -E '"HERE|Assertion|OCW"' /home/kos/control/*.gz >>%%I\control.txt

	echo ********************/home/kos/site_connector/result.log. *zip ****************************** >%%I\site_connector.txt
		plink.exe -pw %%K -ssh root@%%I grep -E '"HERE|Assertion"' /home/kos/site_connector/result.log.00 >>%%I\site_connector.txt
	plink.exe -pw %%K -ssh root@%%I zgrep -E '"HERE|Assertion"' /home/kos/site_connector/*.gz >>%%I\site_connector.txt

	echo ********************/home/kos/storage/result.log. *zip ****************************** >%%I\storage.txt
	plink.exe -pw %%K -ssh root@%%I grep -E '"HERE|Assertion"' /home/kos/storage/result.log.00 >>%%I\storage.txt
	plink.exe -pw %%K -ssh root@%%I zgrep -E '"HERE|Assertion"' /home/kos/storage/*.gz >>%%I\storage.txt

	echo ********************/home/kos/mirror/result.log. *zip ****************************** >%%I\mirror.txt
	plink.exe -pw %%K -ssh root@%%I grep -E '"HERE|Assertion"' /home/kos/mirror/result.log.00 >>%%I\mirror.txt
	plink.exe -pw %%K -ssh root@%%I zgrep -E '"HERE|Assertion"' /home/kos/mirror/*.gz >>%%I\mirror.txt

	echo ********************/home/kos/hlr/result.log. *zip ****************************** >%%I\hlr.txt
	plink.exe -pw %%K -ssh root@%%I grep -E '"HERE|Assertion"' /home/kos/hlr/result.log.00 >>%%I\hlr.txt
	plink.exe -pw %%K -ssh root@%%I zgrep -E '"HERE|Assertion"' /home/kos/hlr/*.gz >>%%I\hlr.txt

	echo ********************/home/kos/hlr/client/result.log. *zip ****************************** >%%I\client.txt
	plink.exe -pw %%K -ssh root@%%I grep -E '"HERE|Assertion"' /home/kos/hlr/client/result.log.00 >>%%I\client.txt
	plink.exe -pw %%K -ssh root@%%I zgrep -E '"HERE|Assertion"' /home/kos/hlr/client/*.gz >>%%I\client.txt

	echo ********************/home/kos/connectivity_tool/result.log. *zip ****************************** >%%I\connectivity_tool.txt
	plink.exe -pw %%K -ssh root@%%I grep -E '"Assertion"' /home/kos/connectivity_tool/result.log.00 >>%%I\connectivity_tool.txt
	plink.exe -pw %%K -ssh root@%%I zgrep -E '"Assertion"' /home/kos/connectivity_tool/*.gz >>%%I\connectivity_tool.txt

	echo ********************/home/kos/cli/result.log. *zip ****************************** >%%I\cli.txt
	plink.exe -pw %%K -ssh root@%%I grep -E '"Assertion|error"' /home/kos/cli/result.log* >>%%I\cli.txt

	echo ********************/home/kos/vi_connector_view. *zip ****************************** >%%I\vi_connector_view.txt
	plink.exe -pw %%K -ssh root@%%I grep -E '"Assertion|error"' /home/kos/vi_connector/logs/vi_connector*.log >>%%I\vi_connector_view.txt	
	plink.exe -pw %%K -ssh root@%%I zgrep -E '"Assertion|error"' /home/kos/vi_connector/logs/vi_connector*.gz >>%%I\vi_connector_view.txt



) 



						

	
)





rem IF exist %%I\1.txt goto end
::PING 1.1.1.1 -n 1 -w 1000 >NUL

rem set v!N!=%%I 





::PING 1.1.1.1 -n 30 -w 1000 >NUL
sleep 10
goto END
:needlist
echo Missing file rpa_list2.txt
goto end
:END

echo done



