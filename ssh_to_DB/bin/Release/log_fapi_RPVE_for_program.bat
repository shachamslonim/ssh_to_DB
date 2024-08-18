@echo on
setLocal EnableDelayedExpansion
title Core watchdog 
IF not exist rpa_list2.txt goto needlist
cls
set I=
::get first errors
For /F "eol=# tokens=1,2,4 delims=; " %%I IN (rpa_list2.txt) DO (

 


 IF not exist %%I mkdir %%I

 IF exist %%I\catalina_old.txt del %%I\catalina_old.txt
 IF exist %%I\control_old.txt del %%I\control_old.txt
 IF exist %%I\fapi_server_old.txt del %%I\fapi_server_old.txt
 IF exist %%I\management_old.txt del %%I\management_old.txt
 IF exist %%I\replication_old.txt del %%I\replication_old.txt
 IF exist %%I\site_connector.txt del %%I\site_connector_old.txt
 IF exist %%I\core.txt del %%I\core.txt



 IF exist %%I\catalina.txt move %%I\catalina.txt %%I\catalina_old.txt
 IF exist %%I\control.txt move %%I\control.txt %%I\control_old.txt
 IF exist %%I\fapi_server.txt move %%I\storage.txt %%I\storage_old.txt
 IF exist %%I\management.txt move %%I\management.txt %%I\management_old.txt
 IF exist %%I\replication.txt move %%I\replication.txt %%I\replication_old.txt
 IF exist %%I\tomcat.txt move %%I\site_connector.txt %%I\site_connector_old.txt


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
					plink.exe -pw %%K -ssh root@%%I grep -E '"Assertion|error|HERE|DEVICE_STATE_SUSPECTED|fatal|deadlock|memory low watermark exceeded"' /scratch/log/kdriver.log.* >> %%I\HERE.txt
					 ) else (
	plink.exe -pw %%K -ssh root@%%I "find / -type f -size +8000k -exec ls -lh  --time-style=long-iso {} \; | grep core\\." >%%I\core.txt
	::IF ERRORLEVEL 1 GOTO End
    plink.exe -pw %%K -ssh root@%%I grep ERROR /home/kos/installationLogs/server.log >%%I\servererror.txt
	echo  ********************/home/kos/replication/result.log%%C%%D********************************* >>%%I\replication.txt
	plink.exe -pw %%K -ssh root@%%I zgrep -E '"HERE|Assertion|DLManager: deadlock suspected|moves to bitmap|highLoadDiskManagerSpace|dirtify volumes|dirtifyVolume|dirtify all"' /home/kos/replication/result.log.*  >>%%I\replication.txt

	echo *********************/home/kos/management/result.log%%C%%D********************************** >>%%I\management.txt
	plink.exe -pw %%K -ssh root@%%I zgrep -E '"HERE|Assertion"' /home/kos/management/result.log.* >>%%I\management.txt

	echo ********************/home/kos/control/result.log%%C%%D************************************** >>%%I\control.txt
	plink.exe -pw %%K -ssh root@%%I zgrep -E '"HERE|Assertion|OCW|alloc took to long"' /home/kos/control/result.log.* >>%%I\control.txt

	echo ********************/home/kos/site_connector/result.log. *zip ****************************** >%%I\site_connector.txt
	plink.exe -pw %%K -ssh root@%%I zgrep -E '"HERE|Assertion"' /home/kos/site_connector/result.log.* >>%%I\site_connector.txt

	echo ********************/home/kos/storage/result.log. *zip ****************************** >%%I\storage.txt
	plink.exe -pw %%K -ssh root@%%I zgrep -E '"HERE|Assertion"' /home/kos/storage/result.log.* >>%%I\storage.txt

	echo ********************/home/kos/mirror/result.log. *zip ****************************** >%%I\mirror.txt
	plink.exe -pw %%K -ssh root@%%I zgrep -E '"HERE|Assertion"' /home/kos/mirror/result.log.* >>%%I\mirror.txt

	echo ********************hlr.log. *zip ****************************** >%%I\hlr.txt
	plink.exe -pw %%K -ssh root@%%I zgrep -E '"HERE|Assertion"' /home/kos/hlr/result.log.* >>%%I\hlr.txt

	echo ********************/home/kos/hlr/client/result.log. *zip ****************************** >%%I\client.txt
	plink.exe -pw %%K -ssh root@%%I zgrep -E '"HERE|Assertion"' /home/kos/hlr/client/result.log* >>%%I\client.txt

	echo ********************/home/kos/connectivity_tool/result.log. *zip ****************************** >%%I\connectivity_tool.txt
	plink.exe -pw %%K -ssh root@%%I zgrep -E '"Assertion"' /home/kos/connectivity_tool/result.log.0* >>%%I\connectivity_tool.txt

rem	echo ********************/home/kos/cli/result.log. *zip ****************************** >%%I\cli.txt
rem	plink.exe -pw %%K -ssh root@%%I grep -E '"Assertion|error"' /home/kos/cli/result.log* >>%%I\cli.txt

	echo ********************/home/kos/connectors. *zip ****************************** >%%I\connectors.txt
	plink.exe -pw %%K -ssh root@%%I zgrep -E '"Assertion|error"' /home/kos/connectors/logs/*.log* >>%%I\connectors.txt
	echo ********************/home/kos/klr/. *zip ****************************** >%%I\klr.txt
	plink.exe -pw %%K -ssh root@%%I zgrep -E '"Assertion|Oops|Loop down|Loop up|eth[0-9]+ NIC Link is|command delay|ERROR: num_outstanding_iocbs is negative!|negative num_pending_cmds|negative num_outstanding_cmds|PANIC BY KASHYA SYSTEM REQUEST|TMD POOL ERROR|WARNING.*vsprintf|quota_sprintf_cat|kernel: f[0-9a-f]{7}|sll_isr: system Error|get_vp_index_for_port_id|unexpected flow|lost frame|sll_extract_best_command went over limits"' /home/kos/klr/result.log* >>%%I\klr.txt



) 



						

	
)





rem IF exist %%I\1.txt goto end
::PING 1.1.1.1 -n 1 -w 1000 >NUL

rem set v!N!=%%I 





::PING 1.1.1.1 -n 30 -w 1000 >NUL

goto END
:needlist
echo Missing file rpa_list2.txt
goto end
:END

echo done



