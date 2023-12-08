// Copyright (c) Johnny Patterson

$dcVersionMajor = 2;
$dcVersionMinor = 1;
$dcVersionRevision = 0;

exec("./scripts/ui.cs");

if (isFile("config/client/dueling.cs"))
	exec("config/client/dueling.cs");

if (!$dcControlsLoaded)
{
	// Declare Dueling binding division
	$remapDivision[$remapCount]	= "Dueling";
	$remapName[$remapCount]	= "Toggle Main Window";
	$remapCmd[$remapCount] = "dcToggleWindow";
	$remapCount++;

	$dcControlsLoaded = 1;
}

function dcExec()
{
	exec("Add-Ons/Client_Dueling/client.cs");
}

function dcToggleWindow(%a)
{
	if (!%a)
		return;

	if (dcDlg.isAwake())
		canvas.popDialog(dcDlg);
	else
		canvas.pushDialog(dcDlg);
}

function dcDlg::setMode(%this, %mode)
{
	%this.currMode = %mode;

	%size = 44;
	%end = 52;

	if (%mode)
	{
		%size += 116;
		%end += 116;
	}

	dcNav.resize(8, 0, %size, 25);
	dcNav_end.resize(%end, 0, 8, 25);

	if (!$dcConnected && %this.currPane != 0)
		%this.setPane(0);
}

function dcDlg::setPane(%this, %pane)
{
	if (%this.currPane == 1 && %pane != 1)
	{
		if ($dcConnected)
			commandToServer('dcCloseDuelPane');

		dcClearDuelPane();
	}

	%this.currPane = %pane;

	%count = %this.paneCount;
	for (%a = 0; %a < %count; %a++)
	{
		%this.pane[%a].setVisible(%a == %pane);
		%this.btn[%a].setValue(%a == %pane);
	}

	dcCheckDataServer.setVisible(0);
	dcMapStats.setVisible(0);
	dcNoDataServer.setVisible(0);

	switch (%pane)
	{
	case 0: // Stats
		if (%this.noDataServer)
		{
			dcNoDataServer.setVisible(1);
		}
		else if (!%this.serverExists)
		{
			dcDataClient().getStats(getNumKeyID());

			if (%this.checkingDataServer)
				dcCheckDataServer.setVisible(1);
		}

		if (%this.isAwake())
		{
			dcStats_Text.forceReflow();
			%y = getWord(dcStats_Container.getPosition(), 1);
			dcStats_Container.resize(0, %y, 378, 12 + getWord(dcStats_Text.getExtent(), 1));
		}
	case 1: // Duel
		dcDuel_AcceptBtn.setActive(!%this.building && !%this.challenging && !%this.dueling && strLen(dcDuel.currChallenge));
		dcDuel_BoastBtn.setActive(!%this.building && !%this.challenging && !%this.dueling);
		dcDuel_Block.setVisible(%this.building || %this.dueling);
		dcDuel_ChallengeBtn.setActive(!%this.building && !%this.challenging && !%this.dueling && strLen(dcDuel.currPlayer) && (dcDuel.playerStatus[dcDuel.currPlayer] == 1 || dcDuel.playerStatus[dcDuel.currPlayer] == 5));
		dcDuel_Challenging.setVisible(%this.challenging);
		dcDuel_DeclineBtn.setActive(strLen(dcDuel.currChallenge) && !dcDuel.boast[dcDuel.currChallenge]);
		commandToServer('dcOpenDuelPane');
	case 2: // Build
		dcMapInfo.setVisible(0);
		dcBuildSessionBtn.setActive(!%this.challenging && !%this.dueling);
		dcBuild_SaveBtn.setActive(%this.building == 2 && dcBuild.brickCount);
		dcBuild_LoadBtn.setActive(%this.building == 2 && strLen(dcBuild.currSave));
		dcBuild_DeleteBtn.setActive(strLen(dcBuild.currSave));
		dcBuild_SubmitBtn.setActive(strLen(dcBuild.currSave));
		dcBuild_TestDuelBtn.setActive(%this.building == 2);
		dcBuild_TestDuelBtn.setText(dcTestDuel.status ? "Stop Test Duel" : "Start Test Duel");

		if (dcBuild.saveStatus[dcBuild.currSave] < 1)
		{
			dcBuild_SubmitBtn.setText("Submit");
			dcBuild_SubmitBtn.command = "dcBuild.submit();";
		}
		else
		{
			dcBuild_SubmitBtn.setText("Withdraw");
			dcBuild_SubmitBtn.command = "dcBuild.withdraw();";
		}

		dcBuild_EditInfoBtn.setActive(strLen(dcBuild.currSave) && dcBuild.saveStatus[dcBuild.currSave] > 0);
		dcBuild_ViewStatsBtn.setActive(strLen(dcBuild.currSave) && dcBuild.saveStatus[dcBuild.currSave] == 2);
		dcBuild_CenterBricksBtn.setActive(dcDlg.building == 2 && dcBuild.brickCount);
		dcBuild_ClearBricksBtn.setActive(dcDlg.building == 2 && dcBuild.brickCount);
		dcTestDuel_StartBtn.setActive(dcTestDuel.memberCount == 2);
	case 3: // Maps
		dcMaps_LoadBtn.setVisible($IAmAdmin);
		dcMaps_LoadBtn.setActive(strLen(dcMaps.currMap) && $IAmAdmin && %this.building == 2);

		if ($IAmAdmin)
			dcMaps_ViewStatsBtn.resize(327, 332, 149, 32);
		else
			dcMaps_ViewStatsBtn.resize(327, 332, 287, 32);

		dcMaps_RevokeBtn.setVisible($IAmAdmin);
		dcMaps_RevokeBtn.setActive(strLen(dcMaps.currMap) && $IAmAdmin);
		dcMaps_ViewStatsBtn.setActive(strLen(dcMaps.currMap));
		dcMaps_ViewSubmissionsBtn.setVisible($IAmAdmin);
		dcMaps_ViewSubmissionsBtn.setActive(dcSubmissionFakeList.rowCount() && $IAmAdmin);
		dcSubmissions_LoadBtn.setActive($IAmAdmin && strLen(dcSubmissions.currSubmission) && %this.building == 2);
		dcSubmissions_NoBtn.setActive($IAmAdmin && strLen(dcSubmissions.currSubmission));
		dcSubmissions_YesBtn.setActive($IAmAdmin && strLen(dcSubmissions.currSubmission));

		if (%this.isAwake() && dcSubmissions.isVisible())
		{
			dcSubmissions_Description.forceReflow();
			dcSubmissions_Container.resize(0, 0, 258, 8 + getWord(dcSubmissions_Description.getExtent(), 1));
		}
	}

	if (%this.isAwake())
	{
		if (dcAlertMsg.isVisible())
		{
			dcAlertMsg_Text.forceReflow();
			dcAlertMsg_Container.resize(0, 0, 328, 8 + getWord(dcAlertMsg_Text.getExtent(), 1));
		}

		if (dcDeclineMsg.isVisible())
		{
			dcDeclineMsg_Preview.forceReflow();
			dcDeclineMsg_Container.resize(0, 0, 328, 8 + getWord(dcDeclineMsg_Preview.getExtent(), 1));
		}
	}
}

function dcPrompt::cmd(%this, %ctrl)
{
	eval(%this.cmd[%ctrl]);

	%this.setVisible(0);
}

function dcPrompt::Ok(%this, %msg, %okCmd)
{
	dcPrompt_Text.setText("\c1" @ %msg);
	%this.cmdBad = "";
	%this.cmdGood = "";
	%this.cmdNo = "";
	%this.cmdOk = %okCmd;
	%this.cmdYes = "";
	dcPrompt_Bad.setVisible(0);
	dcPrompt_Good.setVisible(0);
	dcPrompt_No.setVisible(0);
	dcPrompt_Ok.setVisible(1);
	dcPrompt_Yes.setVisible(0);
	%this.setVisible(1);
}

function dcPrompt::GoodBad(%this, %msg, %goodCmd, %badCmd, %okCmd)
{
	dcPrompt_Text.setText("\c1" @ %msg);
	%this.cmdBad = %badCmd;
	%this.cmdGood = %goodCmd;
	%this.cmdNo = "";
	%this.cmdOk = %okCmd;
	%this.cmdYes = "";
	dcPrompt_Bad.setVisible(1);
	dcPrompt_Good.setVisible(1);
	dcPrompt_No.setVisible(0);
	dcPrompt_Ok.setVisible(1);
	dcPrompt_Yes.setVisible(0);
	%this.setVisible(1);
}

function dcPrompt::YesNo(%this, %msg, %yesCmd, %noCmd)
{
	dcPrompt_Text.setText("\c1" @ %msg);
	%this.cmdBad = "";
	%this.cmdGood = "";
	%this.cmdNo = %noCmd;
	%this.cmdOk = "";
	%this.cmdYes = %yesCmd;
	dcPrompt_Bad.setVisible(0);
	dcPrompt_Good.setVisible(0);
	dcPrompt_No.setVisible(1);
	dcPrompt_Ok.setVisible(0);
	dcPrompt_Yes.setVisible(1);
	%this.setVisible(1);
}

function dcDataClient()
{
	if (!isObject(dcDataClient))
		new TCPObject(dcDataClient);

	return dcDataClient.getID();
}

function dcDataClient::commFailure(%this)
{
	echo("[dcDataClient] Could not connect to data server.");

	if (dcDlg.currPane == 0)
	{
		dcCheckDataServer.setVisible(0);
		dcNoDataServer.setVisible(1);
	}

	dcDlg.checkingDataServer = "";
	dcDlg.noDataServer = 1;
	dcDlg.serverExists = "";
}

function dcDataClient::getStats(%this, %bl_id)
{
	%this.pushCmd("ST", %bl_id | 0);
	%this.lastBL_ID = %bl_id;
}

function dcDataClient::getRankings(%this, %wep)
{
	dcKillDeath_FakeList.clear();
	dcWinLoss_FakeList.clear();
	%this.pushCmd("GR", %wep | 0);
}

function dcDataClient::getWeapons(%this)
{
	%this.pushCmd("WR");
}

function dcDataClient::onConnected(%this)
{
	dcDlg.serverExists = 1;

	if (dcDlg.currPane == 0)
	{
		dcCheckDataServer.setVisible(0);
		dcNoDataServer.setVisible(0);
	}

	%this.send("PING\r\n");
}

function dcDataClient::onConnectFailed(%this)
{
	%this.commFailure();
}

$dcAvatarPrefRestriction = " DecalName FaceName AccentColor ChestColor HatColor HeadColor HipColor LArmColor LHandColor LLegColor PackColor RArmColor RHandcolor RLegcolor SecondPackColor Accent Chest Hat Hip LArm LHand LLeg Pack RArm RHand RLeg SecondPack ";

function dcDataClient::onLine(%this, %line)
{
	%msgType = getField(%line, 0);

	switch$ (%msgType)
	{
	case "PONG": // Generic OK
		%msg = getField(%this.cmd0, 0);
		%args = removeField(%this.cmd0, 0);
		%this.send(%msg TAB %args @ "\r\n");
	case "AD": // Avatar Done
		dcAvatarUpdate(dcAvatar);
	case "AP": // Avatar Preference
		%field = getField(%line, 1);
		%val = getField(%line, 2);
		if (strstr($dcAvatarPrefRestriction, " " @ %field @ " ") != -1)
			dcAvatar.b[%field] = %val;
	case "AR": // Avatar Reset
		dcAvatarReset(dcAvatar);
	case "BS": // Stats
		%bl_id = getField(%line, 1) | 0;
		%name = StripMLControlChars(getField(%line, 2));
		%g = StripMLControlChars(getField(%line, 3));
		%w = StripMLControlChars(getField(%line, 4));

		%txt = "<just:center>- General -";
		%txt = %txt NL "<just:right><rmargin%:55>Total Duels:<just:left><rmargin%:100>" SPC getWord(%g, 0);
		%txt = %txt NL "<just:right><rmargin%:35>Total Wins:<just:left><rmargin%:100>" SPC getWord(%g, 1) @ "<just:right><rmargin%:80>Total Kills:<just:left><rmargin%:100>" SPC getWord(%g, 4);
		%txt = %txt NL "<just:right><rmargin%:35>Total Losses:<just:left><rmargin%:100>" SPC getWord(%g, 2) @ "<just:right><rmargin%:80>Total Deaths:<just:left><rmargin%:100>" SPC getWord(%g, 5);
		%txt = %txt NL "<just:right><rmargin%:35>W/L Ratio:<just:left><rmargin%:100>" SPC getWord(%g, 3) @ "<just:right><rmargin%:80>K/D Ratio:<just:left><rmargin%:100>" SPC getWord(%g, 6);

		%w = strReplace(%w, "|", "\t");

		%count = getFieldCount(%w);
		for (%a = 0; %a < %count; %a++)
		{
			%f = getField(%w, %a);
			%c = getWordCount(%f);
			%n = %f;

			for (%b = 0; %b < 9; %b++)
				%n = removeWord(%n, %c - %b - 1);

			%txt = %txt NL "\n<just:center>-" SPC %n SPC "-<just:left>";
			%txt = %txt NL "<just:right><rmargin%:55>Duels:<just:left><rmargin%:100>" SPC getWord(%f, %c - 9);
			%txt = %txt NL "<just:right><rmargin%:35>Wins:<just:left><rmargin%:100>" SPC getWord(%f, %c - 8) @ "<just:right><rmargin%:80>Kills:<just:left><rmargin%:100>" SPC getWord(%f, %c - 4);
			%txt = %txt NL "<just:right><rmargin%:35>Losses:<just:left><rmargin%:100>" SPC getWord(%f, %c - 7) @ "<just:right><rmargin%:80>Deaths:<just:left><rmargin%:100>" SPC getWord(%f, %c - 3);
			%txt = %txt NL "<just:right><rmargin%:35>W/L Ratio:<just:left><rmargin%:100>" SPC getWord(%f, %c - 6) @ "<just:right><rmargin%:80>K/D Ratio:<just:left><rmargin%:100>" SPC getWord(%f, %c - 2);
			%txt = %txt NL "<just:right><rmargin%:35>Rank:<just:left><rmargin%:100>" SPC getWord(%f, %c - 5) @ "<just:right><rmargin%:80>Rank:<just:left><rmargin%:100>" SPC getWord(%f, %c - 1);
		}

		dcNameTag.setText("\c1" @ %name SPC "(" @ %bl_id @ ")");
		dcStats_Text.setText(%txt);

		if (dcDlg.isAwake() && dcStats.isVisible())
			dcStats_Text.forceReflow();

		%y = getWord(dcStats_Container.getPosition(), 1);
		dcStats_Container.resize(0, %y, 378, 12 + getWord(dcStats_Text.getExtent(), 1));
		%this.popCmd();
	case "RKD": // Rankings: Kills/Deaths
		%txt = removeField(%line, 0);
		%id = getField(%txt, 0);
		%score = getField(%txt, 1) + 0;
		%bl_id = getField(%txt, 2) | 0;
		%name = expandEscape(getField(%txt, 3));
		dcKillDeath_FakeList.setRowById(%id, %score TAB %bl_id TAB %name);
	case "RWL": // Rankings: Wins/Losses
		%txt = removeField(%line, 0);
		%id = getField(%txt, 0);
		%score = getField(%txt, 1) + 0;
		%bl_id = getField(%txt, 2) | 0;
		%name = expandEscape(getField(%txt, 3));
		dcWinLoss_FakeList.setRowById(%id, %score TAB %bl_id TAB %name);
	case "RD": // Rankings: Done
		dcStats.updateRankings();
		%this.popCmd();
	case "WL": // Weapon List
		%txt = removeField(%line, 0);
		dcStats_WeaponSelectList.clear();
		dcStats_WeaponSelectList.add("None", 0);

		%count = getFieldCount(%txt);
		for (%a = 0; %a < %count; %a++)
		{
			%f = getField(%txt, %a);
			%id = getWord(%f, 0);
			%name = removeWord(%f, 0);
			dcStats_WeaponSelectList.add(%name, %id + 1);
		}

		dcStats_WeaponSelectList.setSelected(0);
		dcDlg.weaponsLoaded = 1;
		%this.popCmd();
	}
}

function dcDataClient::ping(%this)
{
	if (dcDlg.checkingDataServer || dcDlg.noDataServer)
		return;

	dcDlg.checkingDataServer = 1;
	dcCheckDataServer.setVisible(1);
	dcDlg.serverExists = "";

	if (!strLen($dcCache::ServerAddress) || !strLen($dcCache::DataPort))
		return %this.commFailure();

	%str = $dcCache::ServerAddress @ ":" @ $dcCache::DataPort;
	%this.connect(%str);
}

function dcDataClient::popCmd(%this)
{
	%cmdCount = %this.cmdCount;
	if (!%cmdCount)
		return;

	dcDlg.checkingDataServer = "";

	for (%a = 0; %a < %cmdCount; %a++)
		%this.cmd[%a] = %this.cmd[%a + 1];

	%this.cmdCount--;

	if (%this.cmdCount)
		%this.ping();
}

function dcDataClient::pushCmd(%this, %msgType, %args)
{
	%cmdCount = %this.cmdCount | 0;
	for (%a = 0; %a < %cmdCount; %a++)
	{
		if (getField(%this.cmd[%a], 0) $= %msgType)
		{
			%this.cmd[%a] = %msgType TAB %args;
			break;
		}
	}

	if (%a >= %cmdCount)
	{
		%this.cmd[%cmdCount] = %msgType TAB %args;
		%this.cmdCount++;
	}

	%this.ping();
}

function dcDataClient::retryPing(%this)
{
	dcDlg.noDataServer = "";
	dcNoDataServer.setVisible(0);
	%this.ping();
}

function dcStats::lookupMe()
{
	dcDataClient().getStats(getNumKeyID());
}

function dcStats::lookupPlayer()
{
	%val = dcLookup.getValue();

	if (!strlen(%val))
		return;

	if (!%val)
		%val = 0;

	dcDlg.setPane(0);
	dcDataClient().getStats(%val);
}

function dcStats::selectWeapon(%this)
{
	%id = dcStats_WeaponSelectList.getSelected();
	%this.currWeapon = %id;

	if (%id)
	{
		dcDataClient().getRankings(%id - 1);
	}
	else
	{
		dcKillDeath_List.deleteAll();
		dcKillDeath_FakeList.clear();
		dcWinLoss_List.deleteAll();
		dcWinLoss_FakeList.clear();
	}
}

function dcStats::updateRankings(%this)
{
	dcKillDeath_List.deleteAll();
	dcWinLoss_List.deleteAll();

	%l = nameToID(dcKillDeath_FakeList);
	%count = %l.rowCount();

	for (%a = 0; %a < %count; %a++)
	{
		%id = %l.getRowId(%a);
		%txt = %l.getRowText(%a);
		%score = getField(%txt, 0) + 0;
		%bl_id = getField(%txt, 1) | 0;
		%name = StripMLControlChars(getField(%txt, 2));

		%tab = new GuiSwatchCtrl()
		{
			position = "1 0";
			extent = "281 30";
			color = "240 240 240 255";

			new GuiMLTextCtrl()
			{
				profile = "dcMediumTextProfile";
				position = "4 6";
				extent = "271 23";
				text = (%id + 1) @ "." SPC %name SPC "(" @ %bl_id @ ")<just:right>" @ %score;
			};
		};

		dcKillDeath_List.add(%tab);

		%tab.resize(1, %a * 32 + 1, 281, 30);

		%btn = new GuiRadioCtrl()
		{
			profile = "dcRadioProfile";
			position = "9 1";
			extent = "250 30";
			groupNum = "0";
			buttonType = "RadioButton";
			command = "dcDataClient().getStats(" @ %bl_id @ ");";
		};

		dcKillDeath_List.add(%btn);

		%btn.resize(9, %a * 32 + 1, 250, 30);
		dcKillDeath_List.pushToBack(%btn);
	}

	%h = %count * 32;
	%y = getWord(dcKillDeath_List.getPosition(), 1);
	dcKillDeath_List.resize(0, %y, 281, %h);

	%l = nameToID(dcWinLoss_FakeList);
	%count = %l.rowCount();

	for (%a = 0; %a < %count; %a++)
	{
		%id = %l.getRowId(%a);
		%txt = %l.getRowText(%a);
		%score = getField(%txt, 0) + 0;
		%bl_id = getField(%txt, 1) | 0;
		%name = StripMLControlChars(getField(%txt, 2));

		%tab = new GuiSwatchCtrl()
		{
			position = "1 0";
			extent = "281 30";
			color = "240 240 240 255";

			new GuiMLTextCtrl()
			{
				profile = "dcMediumTextProfile";
				position = "4 6";
				extent = "271 23";
				text = (%id + 1) @ "." SPC %name SPC "(" @ %bl_id @ ")<just:right>" @ %score;
			};
		};

		dcWinLoss_List.add(%tab);

		%tab.resize(1, %a * 32 + 1, 281, 30);

		%btn = new GuiRadioCtrl()
		{
			profile = "dcRadioProfile";
			position = "9 1";
			extent = "250 30";
			groupNum = "0";
			buttonType = "RadioButton";
			command = "dcDataClient().getStats(" @ %bl_id @ ");";
		};

		dcWinLoss_List.add(%btn);

		%btn.resize(9, %a * 32 + 1, 250, 30);
		dcWinLoss_List.pushToBack(%btn);
	}

	%h = %count * 32;
	%y = getWord(dcWinLoss_List.getPosition(), 1);
	dcWinLoss_List.resize(0, %y, 281, %h);
}

function dcAlertMsg(%id)
{
	dcAlertMsg.currMsg = %id;
	commandToServer('dcAlertMsgRequest', dcAlertMsg.currMsg);
	dcAlertMsg.setVisible(1);
}

function dcAlertMsg::confirm(%this)
{
	commandToServer('dcAlertMsgConfirm', dcAlertMsg.currMsg);
	%this.setVisible(0);
}

function dcBuild::centerBricks(%this)
{
	dcPrompt.YesNo("Center the Bricks?", "commandToServer('dcCenterBricks');");
}

function dcBuild::clearBricks(%this)
{
	dcPrompt.YesNo("Clear the Bricks?", "commandToServer('dcClearBricks');");
}

function dcBuild::delete(%this)
{
	if (strlen(%this.currSave))
		dcPrompt.YesNo("Delete the save?", "dcBuild.deleteConfirm();");
}

function dcBuild::deleteConfirm(%this)
{
	if (strlen(%this.currSave))
		commandToServer('dcDeleteSave', %this.currSave);

	%this.currSave = "";
}

function dcBuild::editInfo(%this, %a)
{
	if (%a)
	{
		for (%a = 0; %a < $dcWeaponCount; %a++)
			%tags = setField(%tags, %a, dcMapInfo.weaponStatus[%a]);

		%count = dcContributerFakeList.rowCount();
		for (%a = 0; %a < %count; %a++)
			%builders = setField(%builders, %a, dcContributerFakeList.getRowText(%a));

		commandToServer('dcSubmitInfo', %tags, %builders);
	}
	else
	{
		commandToServer('dcEditInfo', %this.currSave);
	}
}

function dcBuild::load(%this)
{
	if (strlen(%this.currSave))
		commandToServer('dcLoadSave', %this.currSave);
}

function dcBuild::overwrite(%this)
{
	commandToServer('dcOverwrite');
}

function dcBuild::saveBuild(%this)
{
	%name = dcSaveInput.getValue();

	if (strlen(%name))
		commandToServer('dcSaveBricks', %name);

	dcSaveInput.setValue("");
	dcDlg.setPane(2);
}

function dcBuild::selectSave(%this, %id)
{
	%this.currSave = %id;
	%status = %this.saveStatus[%id];
	%active = strLen(%id);

	dcSaveInput.setValue($dcSaveName[%id]);
	dcBuild_SaveBtn.setActive(dcDlg.building == 2 && %this.brickCount);
	dcBuild_LoadBtn.setActive(dcDlg.building == 2 && %active);
	dcBuild_DeleteBtn.setActive(%active);
	dcBuild_DeleteBtn.setActive(%active);
	dcBuild_SubmitBtn.setActive(%active);

	if (%status < 1)
	{
		dcBuild_SubmitBtn.setText("Submit");
		dcBuild_SubmitBtn.command = "dcBuild.submit();";
	}
	else
	{
		dcBuild_SubmitBtn.setText("Withdraw");
		dcBuild_SubmitBtn.command = "dcBuild.withdraw();";
	}

	dcBuild_EditInfoBtn.setActive(%active && %status > 0);
	dcBuild_ViewStatsBtn.setActive(%active && %status == 2);
}

function dcBuild::submit(%this)
{
	commandToServer('dcSubmit', %this.currSave);
}

function dcBuild::testDuel(%this)
{
	if (dcTestDuel.status)
	{
		commandToServer('dcStopTestDuel');
	}
	else
	{
		dcTestDuel_StartBtn.setActive(dcTestDuel.memberCount == 2);
		dcTestDuel.setVisible(1);
	}
}

function dcBuild::updateSaveList(%this)
{
	%bitmap[-1] = "Add-Ons/Client_Dueling/data/ui/icons/fa-triangle-exclamation-24.png";
	%bitmap[1] = "Add-Ons/Client_Dueling/data/ui/icons/fa-arrow-right-24.png";
	%bitmap[2] = "Add-Ons/Client_Dueling/data/ui/icons/fa-check-24.png";

	%l = nameToID(dcSaveFakeList);
	%count = %l.rowCount();

	for (%a = 0; %a < %count; %a++)
	{
		%id = %l.getRowId(%a);
		%txt = %l.getRowText(%a);
		%name = getField(%txt, 0);
		%rating = getfield(%txt, 1);
		%rG = getWord(%rating, 0);
		%rB = getWord(%rating, 1);

		%btn = %this.saveBtn[%id];
		%icon = %this.saveIcon[%id];
		%status = %this.saveStatus[%id];
		%tab = %this.saveTab[%id];

		if (!isObject(%tab))
		{
			%tab = new GuiSwatchCtrl()
			{
				position = "1 0";
				extent = "275 30";
				color = "240 240 240 255";

				new GuiTextCtrl()
				{
					profile = "dcMediumTextProfile";
					position = "4 3";
					extent = "235 23";
					text = %name;
				};
			};

			%this.saveTab[%id] = %tab;
			dcSaveList.add(%tab);
		}

		%tab.resize(1, %a * 32, 275, 30);

		if (!isObject(%btn))
		{
			%btn = new GuiRadioCtrl()
			{
				profile = "dcRadioProfile";
				position = "9 1";
				extent = "259 30";
				groupNum = "0";
				buttonType = "RadioButton";
				command = "dcBuild.selectSave(" @ %id @ ");";
			};

			%this.saveBtn[%id] = %btn;
			dcSaveList.add(%btn);
		}

		%btn.resize(9, %a * 32, 259, 30);
		dcSaveList.pushToBack(%btn);

		if (%status)
		{
			if (!isObject(%icon))
			{
				%icon = new GuiBitmapCtrl()
				{
					position = "226 3";
					extent = "24 24";

					new GuiBitmapButtonCtrl()
					{
						position = "0 0";
						extent = "24 24";
					};
				};

				%icon.btn = %icon.getObject(0);
				%this.saveIcon[%id] = %icon;
				dcSaveList.add(%icon);
			}

			%icon.resize(227, %a * 32 + 3, 24, 24);
			%icon.setBitmap(%bitmap[%status]);
			dcSaveList.pushToBack(%icon);

			%icon.btn.command = %status == -1 ? "dcAlertMsg(" @ %id @ ");" : "";
		}
		else if (isObject(%icon))
		{
			%icon.delete();
			%this.saveIcon[%id] = "";
		}
	}

	%h = %count * 32 - 2;
	%y = getWord(dcSaveList.getPosition(), 1);
	dcSaveList.resize(0, %y, 276, %h);
}

function dcBuild::viewStats(%this)
{
	commandToServer('dcViewMapStats', %this.currSave);
}

function dcBuild::withdraw(%this, %i)
{
	if (%this.saveStatus[%this.currSave] < 1)
		return 0;

	if (%i)
		commandToServer('dcWithdraw', %this.currSave);
	else
		dcPrompt.YesNo("Withdraw the save?", "dcBuild.withdraw(1);");
}

function dcMapInfo::selectContributer(%this, %id)
{
	%this.currContributer = %id;
	dcMapInfo_RemoveContributerBtn.setActive(strLen(%id));
}

function dcMapInfo::selectWeapon(%this, %id)
{
	%status = !%this.weaponStatus[%id];
	%this.weaponStatus[%id] = %status;

	%color[0] = "240 240 240 255";
	%color[1] = "185 245 185 255";

	%color = %color[%status];
	%this.weaponTab[%id].color = %color;

	for (%a = 0; %a < $dcWeaponCount; %a++)
		%i += %this.weaponStatus[%a];

	dcMapInfo_SubmitBtn.setActive(%i);
}

function dcMapInfo::setContributer(%this, %a)
{
	if (%a)
	{
		%id = dcContributerInput.getValue();

		if (!strLen(%id))
			return 0;

		%idStr = %id;
		%count = 10 - strLen(%id);

		for (%b = 0; %b < %count; %b++)
			%idStr = " " @ %idStr;

		dcContributerFakeList.setRowById(%id, %idStr);
		dcContributerFakeList.sort(0);
		dcMapInfo.updateContributerList();
	}
	else if (dcContributerFakeList.rowCount() > 0)
	{
		dcContributerFakeList.removeRowById(%this.currContributer);
		dcContributerFakeList.sort(0);
		dcMapInfo.updateContributerList();
		dcMapInfo_RemoveContributerBtn.setActive(0);
	}
}

function dcMapInfo::submit(%this)
{
	if (dcBuild.saveStatus[dcBuild.currSave] != 2)
	{
		for (%a = 0; %a < $dcWeaponCount; %a++)
			%tags = setField(%tags, %a, dcMapInfo.weaponStatus[%a]);

		%count = dcContributerFakeList.rowCount();
		for (%a = 0; %a < %count; %a++)
			%builders = setField(%builders, %a, dcContributerFakeList.getRowText(%a));

		commandToServer('dcSubmitInfo', %tags, %builders);
	}
	else
	{
		dcPrompt.YesNo("Edit the save? It'll be resubmitted.", "dcBuild.editInfo(1);");
	}
}

function dcMapInfo::updateContributerList(%this)
{
	%l = nameToID(dcContributerFakeList);
	%count = %l.rowCount();

	for (%a = 0; %a < %count; %a++)
	{
		%bl_id = %l.getRowId(%a);
		%color = "240 240 240 255";

		%btn = %this.contributerBtn[%bl_id];
		%tab = %this.contributerTab[%bl_id];

		if (!isObject(%tab))
		{
			%tab = new GuiSwatchCtrl()
			{
				position = "1 0";
				extent = "210 30";
				color = "240 240 240 255";

				new GuiTextCtrl()
				{
					profile = "dcMediumTextProfile";
					position = "4 3";
					extent = "170 23";
					text = %bl_id;
				};
			};

			%this.contributerTab[%bl_id] = %tab;
			dcContributerList.add(%tab);
		}

		%tab.resize(1, %a * 32, 210, 30);
		%tab.color = %color;

		if (!isObject(%btn))
		{
			%btn = new GuiRadioCtrl()
			{
				profile = "dcRadioProfile";
				position = "-56 1";
				extent = "259 30";
				groupNum = "0";
				buttonType = "RadioButton";
				command = "dcMapInfo.selectContributer(" @ %bl_id @ ");";
			};

			%this.contributerBtn[%bl_id] = %btn;
			dcContributerList.add(%btn);
		}

		%btn.resize(-56, %a * 32, 259, 30);
		dcContributerList.pushToBack(%btn);
	}

	%h = %count * 32 - 2;
	%y = getWord(dcContributerList.getPosition(), 1);
	dcContributerList.resize(0, %y, 212, %h);
}

function dcTestDuel::setStatus(%this, %id, %status)
{
	%this.playerStatus[%id] = %status | 0;

	if (%status)
		%this.memberCount++;
	else
		%this.memberCount--;

	dcTestDuel_StartBtn.setActive(%this.memberCount == 2);
	dcTestDuel.updatePlayerList();
}

function dcTestDuel::start(%this)
{
	if (%this.memberCount != 2)
		return 0;

	%l = nameToID(dcBuildSessionFakeList);
	%count = %l.rowCount();

	for (%a = 0; %a < %count; %a++)
	{
		%id = %l.getRowId(%a);

		if (%this.playerStatus[%id])
			%duelists = setWord(%duelists, %b++ - 1, %id);
	}

	%wep = dcTestDuel_WeaponSelectList.getValue();
	%goal = dcTestDuel_Goal.getValue();
	%this.setVisible(0);
	commandToServer('dcStartTestDuel', %duelists, %wep, %goal);
}

function dcTestDuel::updatePlayerList(%this)
{
	%l = nameToID(dcBuildSessionFakeList);
	%count = %l.rowCount();

	for (%a = 0; %a < %count; %a++)
	{
		%id = %l.getRowId(%a);
		%name = %l.getRowText(%a);

		%btn = %this.playerBtn[%id];
		%status = %this.playerStatus[%id];
		%tab = %this.playerTab[%id];

		if (!isObject(%tab))
		{
			%tab = new GuiSwatchCtrl()
			{
				position = "1 0";
				extent = "179 30";
				color = "240 240 240 255";

				new GuiTextCtrl()
				{
					profile = "dcMediumTextProfile";
					position = "4 3";
					extent = "170 23";
					text = %name;
				};
			};

			%this.playerTab[%id] = %tab;
			dcBuildSessionList.add(%tab);
		}

		%tab.resize(1, %a * 32, 179, 30);

		if (!isObject(%btn))
		{
			%btn = new GuiBitmapButtonCtrl()
			{
				profile = "dcButtonProfile";
				position = "153 3";
				extent = "24 24";
				command = "dcTestDuel.setStatus(1);";
				text = "+";
				buttonType = "PushButton";
				groupNum = "0";
				bitmap = "Add-Ons/Client_Dueling/data/ui/btn";
			};

			%this.playerBtn[%id] = %btn;
			dcBuildSessionList.add(%btn);
		}

		%btn.setVisible((%this.memberCount == 2 && %status) || %this.memberCount != 2);
		%btn.setText(%status ? "-" : "+");
		%btn.command = "dcTestDuel.setStatus(" @ %id @ ", " @ !%status @ ");";
		%btn.resize(153, %a * 32 + 3, 24, 24);
		dcBuildSessionList.pushToBack(%btn);
	}

	%h = %count * 32 - 2;
	%y = getWord(dcBuildSessionList.getPosition(), 1);
	dcBuildSessionList.resize(0, %y, 180, %h);
}

function dcDuel::accept(%this)
{
	commandToServer('dcAccept', %this.currChallenge);
}

function dcDuel::boast(%this, %confirm)
{
	if (%confirm)
	{
		dcChallengeInfo.setVisible(0);
		%goal = dcChallengeInfo_Goal.getValue();
		%wep = dcChallengeInfo_WeaponSelectList.getValue();
		commandToServer('dcBoast', %wep, %goal);
	}
	else
	{
		dcChallengeInfo_Window.setText("Boast");
		dcChallengeInfo.setVisible(1);
		dcChallengeInfo_ConfirmBtn.command = "dcDuel.boast(1);";
	}
}

function dcDuel::cancel(%this)
{
	commandToServer('dcCancel');
}

function dcDuel::challenge(%this, %confirm)
{
	if (%confirm)
	{
		dcChallengeInfo.setVisible(0);
		%goal = dcChallengeInfo_Goal.getValue();
		%wep = dcChallengeInfo_WeaponSelectList.getValue();
		%target = %this.currPlayer;
		commandToServer('dcChallenge', %target, %wep, %goal);
	}
	else
	{
		%l = nameToID(dcPlayerFakeList);
		%count = %l.rowCount();

		for (%a = 0; %a < %count; %a++)
		{
			%id = %l.getRowId(%a);
			%txt = %l.getRowText(%a);

			if (%this.currPlayer == %id)
			{
				%name = getField(%txt, 0);
				break;
			}
		}

		dcChallengeInfo_Window.setText("Challenge -" SPC %name);
		dcChallengeInfo.setVisible(1);
		dcChallengeInfo_ConfirmBtn.command = "dcDuel.challenge(1);";
	}
}

function dcDuel::decline(%this)
{
	commandToServer('dcDecline', %this.currChallenge);
}

function dcDuel::search(%this, %fin)
{
	%this.searchChars = dcDuel_SearchInput.getValue();
	dcDuel.updatePlayerList();

	if (%fin)
		dcDlg.setPane(1);
}

function dcDuel::selectChallenge(%this, %id)
{
	%this.currChallenge = %id;
	dcDuel_AcceptBtn.setActive(!dcDlg.building && !%dcDlg.challenging && !%dcDlg.dueling && %id);
	dcDuel_DeclineBtn.setActive(!dcDlg.building && !dcDlg.challenging && !dcDlg.dueling && !%this.boast[%id]);
}

function dcDuel::selectPlayer(%this, %id)
{
	%this.currPlayer = %id;
	dcDuel_ChallengeBtn.setActive(!dcDlg.building && !dcDlg.challenging && !dcDlg.dueling && strLen(%id) && (%this.playerStatus[%id] == 1 || %this.playerStatus[%id] == 5));
}

function dcDuel::updateChallengeList(%this)
{
	%l = nameToID(dcChallengeFakeList);
	%count = %l.rowCount();

	for (%a = 0; %a < %count; %a++)
	{
		%id = %l.getRowId(%a);
		%txt = %l.getRowText(%a);
		%wep = getField(%txt, 0);
		%goal = getField(%txt, 1);
		%boast = getField(%txt, 2);
		%practice = getField(%txt, 3);
		%player = StripMLControlChars(getField(%txt, 4));

		%btn = %this.challengeBtn[%id];
		%tab = %this.challengeTab[%id];

		if (!isObject(%tab))
		{
			%tab = new GuiSwatchCtrl()
			{
				position = "1 0";
				extent = "275 30";
				color = "240 240 240 255";

				new GuiTextCtrl()
				{
					profile = "dcMediumTextProfile";
					position = "4 3";
					extent = "235 23";
					text = %wep SPC "to" SPC %goal;
				};
			};

			%this.challengeTab[%id] = %tab;
			dcChallengeList.add(%tab);

			if (%practice)
			{
				%p = new GuiMLTextCtrl()
				{
					profile = "dcMediumTextProfile";
					position = "200 -1";
					extent = "50 14";
					text = "<just:right><font:arial:14>Practice";
				};

				%n = new GuiMLTextCtrl()
				{
					profile = "dcMediumTextProfile";
					position = "122 10";
					extent = "128 19";
					text = "<just:right>" @ %player;
				};

				%tab.add(%p);
				%tab.add(%n);
			}

			if (%boast)
				%tab.color = "230 230 255 255";
		}

		%tab.resize(1, %a * 32 + 1, 275, 30);

		if (!isObject(%btn))
		{
			%btn = new GuiRadioCtrl()
			{
				profile = "dcRadioProfile";
				position = "9 1";
				extent = "259 30";
				groupNum = "0";
				buttonType = "RadioButton";
				command = "dcDuel.selectChallenge(" @ %id @ ");";
			};

			%this.challengeBtn[%id] = %btn;
			dcChallengeList.add(%btn);
		}

		%btn.resize(9, %a * 32 + 1, 259, 30);
		dcChallengeList.pushToBack(%btn);
	}

	%h = %count * 32;
	%y = getWord(dcChallengeList.getPosition(), 1);
	dcChallengeList.resize(0, %y, 276, %h);
}

function dcDuel::updatePlayerList(%this)
{
	%l = nameToID(dcPlayerFakeList);
	%count = %l.rowCount();

	for (%a = 0; %a < %count; %a++)
	{
		%id = %l.getRowId(%a);
		%txt = %l.getRowText(%a);
		%name = getField(%txt, 0);

		%btn = %this.playerBtn[%id];
		%tab = %this.playerTab[%id];

		if (strLen(%this.searchChars) && strstr(strLwr(%name), strLwr(%this.searchChars)) == -1)
		{
			if (%this.currPlayer == %id)
			{
				dcDuel_ChallengeBtn.setActive(0);
				%this.selectPlayer();
			}

			if (isObject(%tab))
				%tab.setVisible(0);

			if (isObject(%btn))
				%btn.setVisible(0);

			continue;
		}

		%status = %this.playerStatus[%id];

		if (%status == 1 || %status == 5)
			%color = "240 240 240 255";
		else
			%color = "245 230 230 255";

		if (!isObject(%tab))
		{
			%tab = new GuiSwatchCtrl()
			{
				position = "1 0";
				extent = "275 30";
				color = "240 240 240 255";

				new GuiTextCtrl()
				{
					profile = "dcMediumTextProfile";
					position = "4 3";
					extent = "235 23";
					text = %name;
				};
			};

			%this.playerTab[%id] = %tab;
			dcPlayerList.add(%tab);
		}

		%tab.setVisible(1);
		%tab.resize(1, (%b++ - 1) * 32 + 1, 275, 30);
		%tab.color = %color;

		if (!isObject(%btn))
		{
			%btn = new GuiRadioCtrl()
			{
				profile = "dcRadioProfile";
				position = "9 1";
				extent = "259 30";
				groupNum = "0";
				buttonType = "RadioButton";
				command = "dcDuel.selectPlayer(" @ %id @ ");";
			};

			%this.playerBtn[%id] = %btn;
			dcPlayerList.add(%btn);
		}

		%btn.setVisible(1);
		%btn.resize(9, (%b - 1) * 32 + 1, 259, 30);
		dcPlayerList.pushToBack(%btn);

		%bitmap[2] = "Add-Ons/Client_Dueling/data/ui/icons/fa-hammer-24.png";
		%bitmap[4] = "Add-Ons/Client_Dueling/data/ui/icons/fa-trophy-24.png";
		%bitmap[5] = "Add-Ons/Client_Dueling/data/ui/icons/fa-eye-24.png";

		%icon = %this.playerIcon[%id];

		if (%status > 1 && %status != 3 && %status < 6)
		{
			if (!isObject(%icon))
			{
				%icon = new GuiBitmapCtrl()
				{
					position = "226 3";
					extent = "24 24";

					new GuiBitmapButtonCtrl()
					{
						position = "0 0";
						extent = "24 24";
					};
				};

				%icon.btn = %icon.getObject(0);
				%this.playerIcon[%id] = %icon;
				dcPlayerList.add(%icon);
			}

			%icon.resize(227, %a * 32 + 3, 24, 24);
			%icon.setBitmap(%bitmap[%status]);
			dcPlayerList.pushToBack(%icon);

			%icon.btn.command = (%status == 4 || %status == 5) ? "commandToServer('dcSpectate', " @ %id @ ");" : "";
		}
		else if (isObject(%icon))
		{
			%icon.delete();
			%this.playerIcon[%id] = "";
		}
	}

	%h = %b * 32;
	%y = getWord(dcPlayerList.getPosition(), 1);
	dcPlayerList.resize(0, %y, 276, %h);
}

function dcDeclineMsg()
{
	if (dcMaps.revokeMode == 1)
		dcDeclineWindow.setText("Revoke Message");
	else
		dcDeclineWindow.setText("Decline Message");

	dcDeclineMsg.setVisible(1);
}

function dcDeclineMsg::ok(%this)
{
	dcDeclineMsg_Preview.setText("");

	if (dcDeclineMsg.isVisible() && dcDlg.isAwake())
		dcDeclineMsg_Preview.forceReflow();

	dcDeclineMsg_Container.resize(0, 0, 328, 8 + getWord(dcDeclineMsg_Preview.getExtent(), 1));

	if (dcMaps.revokeMode == 1)
		commandToServer('dcRevokeMap', dcMaps.currMap, dcDeclineMsg_Input.getValue());
	else
		commandToServer('dcReviewSubmission', dcSubmissions.currSubmission, 0, dcDeclineMsg_Input.getValue());

	dcMaps.revokeMode = "";
	dcDeclineMsg_Input.setText("");
	%this.setVisible(0);
}

function dcDeclineMsg::preview(%this)
{
	dcDeclineMsg_Preview.setText(dcDeclineMsg_Input.getValue());

	if (dcDeclineMsg.isVisible() && dcDlg.isAwake())
		dcDeclineMsg_Preview.forceReflow();

	dcDeclineMsg_Container.resize(0, 0, 328, 8 + getWord(dcDeclineMsg_Preview.getExtent(), 1));
}

function dcMapInfo::updateWeaponList(%this)
{
	%l = nameToID(dcWeaponFakeList);
	%count = %l.rowCount();

	%color[0] = "240 240 240 255";
	%color[1] = "185 245 185 255";

	for (%a = 0; %a < %count; %a++)
	{
		%id = %l.getRowId(%a);
		%txt = %l.getRowText(%a);
		%name = getField(%txt, 0);
		%col = %color[%this.weaponStatus[%id] | 0];

		%btn = %this.weaponBtn[%id];
		%tab = %this.weaponTab[%id];

		if (!isObject(%tab))
		{
			%tab = new GuiSwatchCtrl()
			{
				position = "1 0";
				extent = "210 30";
				color = "240 240 240 255";

				new GuiTextCtrl()
				{
					profile = "dcMediumTextProfile";
					position = "4 3";
					extent = "170 23";
					text = %name;
				};
			};

			%this.weaponTab[%id] = %tab;
			dcMapInfo_WeaponList.add(%tab);
		}

		%tab.resize(1, %a * 32, 210, 30);
		%tab.color = %col;

		if (!isObject(%btn))
		{
			%btn = new GuiRadioCtrl()
			{
				profile = "dcRadioProfile";
				position = "9 1";
				extent = "194 30";
				groupNum = "0";
				buttonType = "RadioButton";
				command = "dcMapInfo.selectWeapon(" @ %id @ ");";
			};

			%this.weaponBtn[%id] = %btn;
			dcMapInfo_WeaponList.add(%btn);
		}

		%btn.resize(9, %a * 32, 194, 30);
		dcMapInfo_WeaponList.pushToBack(%btn);
	}

	%h = %count * 32 - 2;
	%y = getWord(dcMapInfo_WeaponList.getPosition(), 1);
	dcMapInfo_WeaponList.resize(0, %y, 212, %h);
}

function dcMaps::load(%this)
{
	if (strLen(%this.currMap))
		commandToServer('dcLoadMap', %this.currMap);
}

function dcMaps::revoke(%this)
{
	if (strLen(%this.currMap))
	{
		%this.revokeMode = 1;
		dcPrompt.YesNo("Are you sure?", "dcDeclineMsg();");
	}
}

function dcMaps::selectMap(%this, %id)
{
	%this.currMap = %id;
	dcMaps_LoadBtn.setActive(dcDlg.building == 2 && strLen(%this.currMap) && $IAmAdmin);
	dcMaps_RevokeBtn.setActive(strLen(%this.currMap) && $IAmAdmin);
	dcMaps_ViewStatsBtn.setActive(strLen(%this.currMap));
}

function dcMaps::selectWeapon(%this, %id)
{
	deleteVariables("$dcMap*");

	%this.currMap = "";
	dcMaps_ViewStatsBtn.setActive(0);

	%this.currWeapon = %id;
	dcMapFakeList.clear();
	dcMapList.deleteAll();
	commandToServer('dcViewMaps', %id);
}

function dcMaps::updateMapList(%this)
{
	%l = nameToID(dcMapFakeList);
	%count = %l.rowCount();

	for (%a = 0; %a < %count; %a++)
	{
		%id = %l.getRowId(%a);
		%txt = %l.getRowText(%a);
		%name = getField(%txt, 0);
		%rating = getField(%txt, 1);
		%rG = getWord(%rating, 0);
		%rB = getWord(%rating, 1);

		%btn = %this.mapBtn[%id];
		%rating = %this.mapRating[%id];
		%tab = %this.mapTab[%id];

		if (!isObject(%tab))
		{
			%tab = new GuiSwatchCtrl()
			{
				position = "1 0";
				extent = "274 30";
				color = "240 240 240 255";

				new GuiTextCtrl()
				{
					profile = "dcMediumTextProfile";
					position = "4 3";
					extent = "235 23";
					text = %name;
				};

				new GuiSwatchCtrl()
				{
					position = "4 24";
					extent = "267 2";
					color = "255 190 190 255";

					new GuiSwatchCtrl()
					{
						position = "0 0";
						extent = "0 2";
						color = "185 245 185 255";
						minExtent = "0 0";
					};
				};
			};

			%rating = %tab.getObject(1);
			%this.mapRating[%id] = %rating;
			%this.mapTab[%id] = %tab;
			dcMapList.add(%tab);
		}

		%tab.resize(1, %a * 32, 274, 30);
		%rT = %rG + %rB;

		if (%rT)
			%p = %rG / %rT;
		else
			%p = 0.5;

		%rating.getObject(0).resize(0, 0, %p * 267, 2);

		if (!isObject(%btn))
		{
			%btn = new GuiRadioCtrl()
			{
				profile = "dcRadioProfile";
				position = "9 1";
				extent = "259 30";
				groupNum = "0";
				buttonType = "RadioButton";
				command = "dcMaps.selectMap(" @ %id @ ");";
			};

			%this.mapBtn[%id] = %btn;
			dcMapList.add(%btn);
		}

		%btn.resize(9, %a * 32, 259, 30);
		dcMapList.pushToBack(%btn);
	}

	%h = %count * 32 - 2;
	%y = getWord(dcMapList.getPosition(), 1);
	dcMapList.resize(0, %y, 276, %h);
}

function dcMaps::updateWeaponList(%this)
{
	%l = nameToID(dcWeaponFakeList);
	%count = %l.rowCount();

	for (%a = 0; %a < %count; %a++)
	{
		%id = %l.getRowId(%a);
		%txt = %l.getRowText(%a);
		%name = getField(%txt, 0);

		%btn = %this.weaponBtn[%id];
		%tab = %this.weaponTab[%id];

		if (!isObject(%tab))
		{
			%tab = new GuiSwatchCtrl()
			{
				position = "1 0";
				extent = "275 30";
				color = "240 240 240 255";

				new GuiTextCtrl()
				{
					profile = "dcMediumTextProfile";
					position = "4 3";
					extent = "235 23";
					text = %name;
				};
			};

			%this.weaponTab[%id] = %tab;
			dcWeaponList.add(%tab);
		}

		%tab.resize(1, %a * 32 + 1, 275, 30);

		if (!isObject(%btn))
		{
			%btn = new GuiRadioCtrl()
			{
				profile = "dcRadioProfile";
				position = "9 1";
				extent = "259 30";
				groupNum = "0";
				buttonType = "RadioButton";
				command = "dcMaps.selectWeapon(" @ %id @ ");";
			};

			%this.weaponBtn[%id] = %btn;
			dcWeaponList.add(%btn);
		}

		%btn.resize(9, %a * 32 + 1, 259, 30);
		dcWeaponList.pushToBack(%btn);
	}

	%h = %count * 32;
	%y = getWord(dcWeaponList.getPosition(), 1);
	dcWeaponList.resize(0, %y, 276, %h);
}

function dcMaps::viewStats(%this)
{
	commandToServer('dcViewMapStats', %this.currMap);
}

function dcMaps::viewSubmissions(%this)
{
	if (!$IAmAdmin)
		return;

	%active = strLen(dcSubmissions.currSubmission);
	dcSubmissions_LoadBtn.setActive(%active && dcDlg.building == 2);
	dcSubmissions_NoBtn.setActive(%active);
	dcSubmissions_YesBtn.setActive(%active);
	dcSubmissions.setVisible(1);
}

function dcSubmissions::load(%this)
{
	if (strLen(%this.currSubmission))
		commandToServer('dcLoadSubmission', %this.currSubmission);
}

function dcSubmissions::no(%this)
{
	if (strLen(%this.currSubmission))
	{
		%this.revokeMode = 0;
		dcPrompt.YesNo("Are you sure?", "dcDeclineMsg();");
	}
}

function dcSubmissions::selectSubmission(%this, %id)
{
	dcSubmissions_Description.deleteAll();
	%this.currSubmission = %id;
	%active = strLen(%id);
	dcSubmissions_LoadBtn.setActive(%active && dcDlg.building == 2);
	dcSubmissions_NoBtn.setActive(%active);
	dcSubmissions_YesBtn.setActive(%active);

	if (%active)
		commandToServer('dcViewSubmissionInfo', %id);
}

function dcSubmissions::updateSubmissionList(%this)
{
	%l = nameToID(dcSubmissionFakeList);
	%count = %l.rowCount();

	for (%a = 0; %a < %count; %a++)
	{
		%id = %l.getRowId(%a);
		%name = %l.getRowText(%a);

		%btn = %this.submissionBtn[%id];
		%tab = %this.submissionTab[%id];

		if (!isObject(%tab))
		{
			%tab = new GuiSwatchCtrl()
			{
				position = "1 0";
				extent = "210 30";
				color = "240 240 240 255";

				new GuiTextCtrl()
				{
					profile = "dcMediumTextProfile";
					position = "4 3";
					extent = "170 23";
					text = %name;
				};
			};

			%this.submissionTab[%id] = %tab;
			dcSubmissionList.add(%tab);
		}

		%tab.resize(1, %a * 32, 210, 30);
		%tab.color = "240 240 240 255";

		if (!isObject(%btn))
		{
			%btn = new GuiRadioCtrl()
			{
				profile = "dcRadioProfile";
				position = "-56 1";
				extent = "259 30";
				groupNum = "0";
				buttonType = "RadioButton";
				command = "dcSubmissions.selectSubmission(" @ %id @ ");";
			};

			%this.submissionBtn[%id] = %btn;
			dcSubmissionList.add(%btn);
		}

		%btn.resize(-56, %a * 32, 259, 30);
		dcSubmissionList.pushToBack(%btn);
	}

	%h = %count * 32;

	if (dcDlg.isAwake() && %this.isVisible())
		dcSubmissions_Description.forceReflow();

	%y = getWord(dcSubmissionList.getPosition(), 1);
	dcSubmissionList.resize(0, %y, 212, %h);
}

function dcSubmissions::yes(%this)
{
	if (strLen(%this.currSubmission))
		dcPrompt.YesNo("Are you sure?", "commandToServer('dcReviewSubmission', dcSubmissions.currSubmission, 1);");
}

function clientCmddcAlertMsg(%msg)
{
	if (!strLen(%msg))
		%msg = "We're sorry, it's just not what we're looking for.";

	dcAlertMsg_Text.setText(%msg);
	dcDlg.setPane(2);
}

function clientCmddcCloseWindow()
{
	if (dcDlg.isAwake())
		dcToggleWindow(1);
}

function clientCmddcConfirmedInfoSubmission()
{
	dcMapInfo.setVisible(0);
}

function clientCmddcLoadBackup()
{
	if (!dcDlg.isAwake())
		dcToggleWindow(1);

	dcPrompt.YesNo("Load your backup save?", "commandToServer('dcLoadBackup', 1);", "commandToServer('dcLoadBackup', 0);");
}

function clientCmddcOverwrite()
{
	dcPrompt.YesNo("Overwrite the save?", "dcBuild.overwrite();", "");
}

function clientCmddcPing(%port, %ip)
{
	$dcConnected = 1;

	if (!strLen(%ip))
		%ip = serverConnection.getRawIP();

	$dcCache::ServerAddress = %ip;
	$dcCache::DataPort = %port;

	export("$dcCache::*", "config/client/dueling.cs");
	commandToServer('dcPong', $dcVersionMajor, $dcVersionMinor, $dcVersionRevision);
}

function clientCmddcRefreshMaps(%wep)
{
	if (dcMaps.currWeapon == %wep)
		commandToServer('dcViewMaps', %wep);
}

function clientCmddcRefreshRankings(%wep)
{
	%dc = dcDataClient();

	if (dcStats.currWeapon == %wep && strLen(dcStats.currWeapon))
		%dc.getRankings(%wep - 1);
}

function clientCmddcRefreshStats()
{
	%bl_id = getNumKeyID();
	%dc = dcDataClient();

	if (%dc.lastBL_ID == %bl_id)
		%dc.getStats(%bl_id);
}

function clientCmddcRequestInfo(%save, %tags, %contributers)
{
	%count = getFieldCount(%tags);

	for (%a = 0; %a < %count; %a++)
	{
		%status = getField(%tags, %a) | 0;
		dcMapInfo.weaponStatus[%a] = %status;
		%i += %status;
	}

	dcContributerList.deleteAll();
	dcContributerFakeList.clear();

	%count = getFieldCount(%contributers);

	for (%a = 0; %a < %count; %a++)
	{
		%bl_id = getField(%contributers, %a) | 0;
		%bl_idStr = %bl_id;

		%len = 10 - strLen(%bl_id);

		for (%b = 0; %b < %len; %b++)
			%bl_idStr = " " @ %bl_idStr;

		dcContributerFakeList.setRowById(%bl_id, %bl_idStr);
	}

	dcContributerFakeList.sort(0);
	dcMapInfo.updateContributerList();

	dcMapInfo_Window.setText("Map Info -" SPC $dcSaveName[%save]);
	dcDlg.setPane(2);
	dcMapInfo.updateWeaponList();
	dcMapInfo_SubmitBtn.setActive(%i);
	dcMapInfo.setVisible(1);
	dcMapInfo.currContributer = "";
	dcMapInfo_RemoveContributerBtn.setActive(0);
}

function clientCmddcRemoveBuildSessionMember(%id)
{
	if (dcTestDuel.playerStatus[%id])
		dcTestDuel.memberCount--;

	dcTestDuel_StartBtn.setActive(0);
	dcTestDuel.playerStatus[%id] = "";
	dcBuildSessionFakeList.removeRowById(%id);
	dcBuildSessionFakeList.sort(0);

	%btn = dcTestDuel.playerBtn[%id];
	%tab = dcTestDuel.playerTab[%id];

	if (isObject(%tab))
		%tab.delete();
	if (isObject(%btn))
		%btn.delete();

	dcTestDuel.playerBtn[%id] = "";
	dcTestDuel.playerTab[%id] = "";

	dcTestDuel.updatePlayerList();
}

function clientCmddcRemoveChallenge(%id)
{
	dcChallengeFakeList.removeRowById(%id);
	dcChallengeFakeList.sort(0);

	%btn = dcDuel.challengeBtn[%id];
	%tab = dcDuel.challengeTab[%id];

	if (isObject(%btn))
		%btn.delete();
	if (isObject(%tab))
		%tab.delete();

	dcDuel.challengeBtn[%id] = "";
	dcDuel.challengeTab[%id] = "";

	if (dcDuel.currChallenge == %id)
	{
		dcDuel.currChallenge = "";
		dcDuel_AcceptBtn.setActive(0);
		dcDuel_DeclineBtn.setActive(0);
	}

	dcDuel.updateChallengeList();

	dcDuel.boast[%id] = "";
	dcDuel.goal[%id] = "";
	dcDuel.practice[%id] = "";
	dcDuel.target[%id] = "";
	dcDuel.weapon[%id] = "";
}

function clientCmddcRemovePlayer(%id)
{
	dcDuel.playerStatus[%id] = "";
	dcPlayerFakeList.removeRowById(%id);
	dcPlayerFakeList.sort(0);

	%btn = dcDuel.playerBtn[%id];
	%icon = dcDuel.playerIcon[%id];
	%tab = dcDuel.playerTab[%id];

	if (isObject(%tab))
		%tab.delete();
	if (isObject(%btn))
		%btn.delete();
	if (isObject(%icon))
		%icon.delete();

	dcDuel.playerBtn[%id] = "";
	dcDuel.playerIcon[%id] = "";
	dcDuel.playerTab[%id] = "";

	if (dcDuel.currPlayer == %id && strLen(dcDuel.currPlayer))
	{
		dcDuel.selectPlayer();
		dcChallengeInfo.setVisible(0);
	}

	dcDuel.updatePlayerList();
}

function clientCmddcRemoveSave(%id)
{
	dcBuild.saveStatus[%id] = "";
	$dcSaveName[%id] = "";
	dcSaveFakeList.removeRowById(%id);
	dcSaveFakeList.sort(0);

	%btn = dcBuild.saveBtn[%id];
	%icon = dcBuild.saveIcon[%id];
	%tab = dcBuild.saveTab[%id];

	if (isObject(%btn))
		%btn.delete();

	if (isObject(%tab))
		%tab.delete();

	if (isObject(%icon))
		%icon.delete();

	dcBuild.saveBtn[%id] = "";
	dcBuild.saveIcon[%id] = "";
	dcBuild.saveTab[%id] = "";

	if (dcBuild.currSave == %id)
		dcBuild.currSave = "";

	%currSave = dcBuild.currSave;
	%active = strLen(%currSave);
	%status = dcBuild.saveStatus[%currSave];

	dcBuild_LoadBtn.setActive(0);
	dcBuild_DeleteBtn.setActive(0);
	dcBuild_DeleteBtn.setActive(%active);
	dcBuild_SubmitBtn.setActive(%active);

	if (%status)
	{
		dcBuild_SubmitBtn.setText("Withdraw");
		dcBuild_SubmitBtn.command = "dcBuild.withdraw();";
	}
	else
	{
		dcBuild_SubmitBtn.setText("Submit");
		dcBuild_SubmitBtn.command = "dcBuild.submit();";
	}

	dcBuild_EditInfoBtn.setActive(%active && %status > 0);
	dcBuild_ViewStatsBtn.setActive(%active && %status == 2);

	dcBuild.updateSaveList();
}

function clientCmddcRemoveMap(%id)
{
	dcMapFakeList.removeRowById(%id);
	dcMapFakeList.sort(0);

	%btn = dcMaps.mapBtn[%id];
	%tab = dcMaps.mapTab[%id];

	if (isObject(%tab))
		%tab.delete();
	if (isObject(%btn))
		%btn.delete();

	dcMaps.mapBtn[%id] = "";
	dcMaps.mapRating[%id] = "";
	dcMaps.mapTab[%id] = "";

	if (dcMaps.currMap == %id)
		dcMaps.selectMap();

	dcMaps.updateMapList();
}

function clientCmddcRemoveSubmission(%id)
{
	dcSubmissionFakeList.removeRowById(%id);
	dcSubmissionFakeList.sort(0);

	%btn = dcSubmissions.submissionBtn[%id];
	%tab = dcSubmissions.submissionTab[%id];

	if (isObject(%btn))
		%btn.delete();
	if (isObject(%tab))
		%tab.delete();

	dcSubmissions.submissionBtn[%id] = "";
	dcSubmissions.submissionTab[%id] = "";

	if (dcSubmissions.currSubmission == %id)
	{
		dcSubmissions_Description.setText("");

		if (dcDlg.isAwake() && dcSubmissions.isVisible())
			dcSubmissions_Description.forceReflow();

		dcSubmissions_Container.resize(0, 0, 258, 16 + getWord(dcSubmissions_Description.getExtent(), 1));
		dcSubmissions.selectSubmission();
	}

	dcSubmissions.updateSubmissionList();
	%count = dcSubmissionFakeList.rowCount();
	dcMaps_ViewSubmissionsBtn.setText("Submissions (" @ %count @ ")");
	dcMaps_ViewSubmissionsBtn.setActive(%count && $IAmAdmin);
}

function clientCmddcSaveExists()
{
	dcPrompt.Ok("There is a save by that name.");
}

function clientCmddcSaveLimit()
{
	dcPrompt.Ok("You have reached the save limit.");
}

function clientCmddcMapRating()
{
	dcPrompt.GoodBad("Was your last map good?", "commandToServer('dcMapRating', 1);", "commandToServer('dcMapRating', -1);", "commandToServer('dcMapRating', 0);");
}

function clientCmddcSetBrickCount(%count, %max)
{
	%count = StripMLControlChars(%count);
	%max = StripMLControlChars(%max);
	%active = dcDlg.building == 2 && %count;

	dcBuild.brickCount = %count;
	$dcBrickMax = %max;
	dcBuild_SaveBtn.setActive(%active);
	dcBuild_CenterBricksBtn.setActive(%active);
	dcBuild_ClearBricksBtn.setActive(%active);
	dcBrickCount.setText("<just:center><color:ffffff>Bricks: " @ %count @ "/" @ %max @ " ");
}

function clientCmddcSetBuilding(%status)
{
	if (%status == 2)
	{
		dcTestDuel.memberCount = "";
		dcBuildSessionFakeList.clear();
		dcBuildSessionList.deleteAll();
	}

	dcDlg.building = %status;
	dcBrickCount.setVisible(%status);
	dcBuild_SaveBtn.setActive(%status == 2);
	dcBuild_LoadBtn.setActive(%status == 2 && strLen(dcBuild.currSave));
	dcBuild_CenterBricksBtn.setActive(%status == 2 && dcBuild.brickCount);
	dcBuild_ClearBricksBtn.setActive(%status == 2 && dcBuild.brickCount);

	if (%status)
	{
		dcBuildSessionBtn.setText("Leave Session");
		dcBuildSessionBtn.command = "dcPrompt.YesNo(\"Leave the Build Session?\", \"commandToServer('dcStopBuilding');\");";
	}
	else
	{
		dcBuildSessionBtn.setText("Start Session");
		dcBuildSessionBtn.command = "dcPrompt.YesNo(\"Start a Build Session?\", \"commandToServer('dcStartBuilding');\");";
	}

	if (dcDlg.isAwake())
		canvas.popDialog(dcDlg);
}

function clientCmddcSetBuildSessionMember(%id, %name)
{
	if (dcBuildSessionFakeList.getRowNumById(%id) == -1)
		dcTestDuel.playerStatus[%id] = "";

	dcBuildSessionFakeList.setRowById(%id, %name);
	dcBuildSessionFakeList.sort(0);
	dcTestDuel.updatePlayerList();
	dcDuel_Block.setVisible(1);
}

function clientCmddcSetChallenge(%id, %weapon, %goal, %practice, %target)
{
	%boast = !%practice;

	dcChallengeFakeList.setRowById(%id, %weapon TAB %goal TAB %boast TAB %practice TAB %target);
	dcChallengeFakeList.sort(0);
	dcDuel.updateChallengeList();

	dcDuel.boast[%id] = %boast;
	dcDuel.goal[%id] = %goal;
	dcDuel.practice[%id] = %practice;
	dcDuel.target[%id] = %player;
	dcDuel.weapon[%id] = %weapon;
}

function clientCmddcSetChallenging(%status, %wep, %goal, %practice, %target)
{
	%goal += 0;
	%status += 0;
	%target = StripMLControlChars(%target);
	%wep = StripMLControlChars(%wep);

	%boast = !%practice;

	if (%status == 1)
	{
		if (%boast)
			%targetTxt = "\c4Boast";
		else
			%targetTxt = "\c4" @ %target;

		if (%practice)
			%practiceTxt = "\c5Practice";

		dcDuel_ChallengeDescription.setText("<just:center>" @ %wep SPC "\c2to\c0" SPC %goal NL %targetTxt NL %practiceTxt);
	}
	else if (%status == 2)
	{
		%decline = 1;
		%status = 0;
	}

	dcDlg.challenging = %status;
	dcDlg.setPane(1);

	if (%decline)
		dcPrompt.Ok("Your challenge was declined.");
}

function clientCmddcSetDueling(%status)
{
	dcDlg.dueling = %status;
	dcBuildSessionBtn.setActive(0);
	dcDuel_AcceptBtn.setActive(0);
	dcDuel_BoastBtn.setActive(0);
	dcDuel_ChallengeBtn.setActive(0);
	dcDuel_Block.setVisible(1);
}

function clientCmddcSetMode(%mode)
{
	dcDlg.setMode(%mode);
}

function clientCmddcSetPlayer(%id, %name, %status)
{
	%status += 0;
	dcDuel.playerStatus[%id] = %status;

	%currPlayer = dcDuel.currPlayer;

	if (%id == %currPlayer && strLen(%currPlayer) && %status == 5)
		dcChallengeInfo.setVisible(0);

	dcPlayerFakeList.setRowById(%id, %name);
	dcPlayerFakeList.sort(0);
	dcDuel.updatePlayerList();
	dcDuel_ChallengeBtn.setActive(!dcDlg.building && strLen(%currPlayer)&& (dcDuel.playerStatus[%currPlayer] == 1 || dcDuel.playerStatus[%currPlayer] == 5));
}

function clientCmddcSetSave(%id, %name, %status, %rG, %rB)
{
	$dcSaveName[%id] = %name;
	dcBuild.saveStatus[%id] = %status;
	dcSaveFakeList.setRowById(%id, %name TAB %rG SPC %rB);
	dcSaveFakeList.sort(0);
	dcBuild.updateSaveList();

	%currSave = dcBuild.currSave;
	%active = strLen(%currSave);
	%status = dcBuild.saveStatus[%currSave];

	dcBuild_DeleteBtn.setActive(%active);
	dcBuild_SubmitBtn.setActive(%active);

	if (%status > 0)
	{
		dcBuild_SubmitBtn.setText("Withdraw");
		dcBuild_SubmitBtn.command = "dcBuild.withdraw();";
	}
	else
	{
		dcBuild_SubmitBtn.setText("Submit");
		dcBuild_SubmitBtn.command = "dcBuild.submit();";
	}

	dcBuild_EditInfoBtn.setActive(%active && %status > 0);
	dcBuild_ViewStatsBtn.setActive(%active && %status == 2);
}

function clientCmddcSetMap(%id, %name, %rG, %rB)
{
	$dcMapName[%id] = %name;
	$dcMapRating[%id] = %rG SPC %rB;
	dcMapFakeList.setRowById(%id, %name TAB %rG SPC %rB);
	dcMapFakeList.sort(0);
	dcMaps.updateMapList();

	%active = strLen(dcMaps.currMap);

	dcMaps_ViewStatsBtn.setActive(%active);
	dcMaps_LoadBtn.setActive(%active && $IAmAdmin);
	dcMaps_RevokeBtn.setActive(%active && $IAmAdmin);
}

function clientCmddcSetSubmission(%id, %name)
{
	dcSubmissionFakeList.setRowById(%id, %name);
	dcSubmissionFakeList.sort(0);
	dcSubmissions.updateSubmissionList();
	%count = dcSubmissionFakeList.rowCount();
	dcMaps_ViewSubmissionsBtn.setText("Submissions (" @ %count @ ")");
	dcMaps_ViewSubmissionsBtn.setActive(%count && $IAmAdmin);
}

function clientCmddcSetTestDuelStatus(%status)
{
	dcTestDuel.status = %status;
	dcBuild_TestDuelBtn.setText(%status ? "Stop Test Duel" : "Start Test Duel");
}

function clientCmddcSetWeapons(%list)
{
	dcTestDuel_WeaponSelectList.clear();
	dcChallengeInfo_WeaponSelectList.clear();
	dcWeaponFakeList.clear();

	$dcWeaponCount = 0;

	%count = getFieldCount(%list);
	for (%a = 0; %a < %count; %a++)
	{
		%weapon = getField(%list, %a);
		$dcWeapon[$dcWeaponCount++ - 1] = %weapon;
		dcWeaponFakeList.setRowById(%a, %weapon.uiName);
	}

	dcWeaponFakeList.sort(0);
	dcMaps.updateWeaponList();

	for (%a = 0; %a < $dcWeaponCount; %a++)
	{
		%txt = dcWeaponFakeList.getRowText(%a);
		%name = getField(%txt, 0);
		dcChallengeInfo_WeaponSelectList.add(%name, %a);
		dcTestDuel_WeaponSelectList.add(%name, %a);
	}

	dcChallengeInfo_WeaponSelectList.setSelected(0);
	dcTestDuel_WeaponSelectList.setSelected(0);
}

function clientCmddcShowMapStats(%name, %builders, %duels, %tags, %rG, %rB, %approver, %id)
{
	if (!dcDlg.isAwake())
		dcToggleWindow(1);

	dcMapStats_Window.setText("Stats -" SPC %name SPC "- ID:" SPC %id);
	%rT = %rG + %rB;

	if (%rT)
		%p = %rG / %rT;
	else
		%p = 0.5;

	dcMapStats_Rating.resize(%offset, 0, %p * 238, 12);

	%count = getFieldCount(%builders);
	%text = %count > 1 ? "Builders:" : "Builder:";

	%listCtrl = new GuiTextListCtrl()
	{
		profile = "GuiTextListProfile";
		position = "0 0";
		extent = "0 0";
		minExtent = "8 2";
		visible = "0";
		sortedNumerical = "1";
		sortedAsc = "1";
		sortedBy = "1";
	};

	for (%a = 0; %a < %count; %a++)
	{
		%field = getField(%builders, %a);
		%bl_id = getWord(%field, 0);
		%name = removeWord(%field, 0);
		%listCtrl.setRowById(%bl_id, %name);
	}

	%listCtrl.sort(0);

	for (%a = 0; %a < %count; %a++)
	{
		%bl_id = %listCtrl.getRowId(%a);
		%name = %listCtrl.getRowText(%a);
		%text = %text NL "	  " @ %name SPC "(" @ %bl_id @ ")";
	}

	%listCtrl.delete();

	%l = nameToID(dcWeaponFakeList);
	%count = %l.rowCount();

	for (%a = 0; %a < %count; %a++)
	{
		%id = %l.getRowId(%a);
		%txt = %l.getRowText(%a);
		%name = getField(%txt, 0);

		if (getField(%tags, %id))
		{
			%wepStr = %wepStr NL "	  " @ %name;
			%w++;
		}
	}

	%bl_id = getWord(%approver, 0);
	%name = removeWord(%approver, 0);

	%text = %text NL (%w > 1 ? "Weapons:" : "Weapon:") @ %wepStr NL "Total Duels:" NL "	  " @ %duels NL "Approval:" NL "	  " @ %name SPC "(" @ %bl_id @ ")";
	dcMapStats.setVisible(1);
	dcMapStats_Text.setText(%text);

	if (dcDlg.isAwake())
		dcMapStats_Text.forceReflow();

	dcMapStats_Container.resize(0, 0, 258, 45 + getWord(dcMapStats_Text.getExtent(), 1));
}

function clientCmddcShowSubmissionInfo(%builders, %tags)
{
	%count = getFieldCount(%builders);
	%text = %count > 1 ? "Builders:" : "Builder:";

	%listCtrl = new GuiTextListCtrl()
	{
		profile = "GuiTextListProfile";
		position = "0 0";
		extent = "0 0";
		visible = "0";
		sortedNumerical = "1";
		sortedAsc = "1";
		sortedBy = "1";
	};

	for (%a = 0; %a < %count; %a++)
	{
		%field = getField(%builders, %a);
		%bl_id = getWord(%field, 0);
		%name = removeWord(%field, 0);
		%listCtrl.setRowById(%bl_id, %name);
	}

	%listCtrl.sort(0);

	for (%a = 0; %a < %count; %a++)
	{
		%bl_id = %listCtrl.getRowId(%a);
		%name = %listCtrl.getRowText(%a);
		%text = %text NL "	  " @ %name SPC "(" @ %bl_id @ ")";
	}

	%listCtrl.delete();

	%l = nameToID(dcWeaponFakeList);
	%count = %l.rowCount();

	for (%a = 0; %a < %count; %a++)
	{
		%id = %l.getRowId(%a);
		%txt = %l.getRowText(%a);
		%name = getField(%txt, 0);

		if (getField(%tags, %id))
		{
			%wepStr = %wepStr NL "	  " @ %name;
			%w++;
		}
	}

	%text = %text NL (%w > 1 ? "Weapons:" : "Weapon:") @ %wepStr;
	dcSubmissions_Description.setText(%text);

	if (dcDlg.isAwake() && dcSubmissions.isVisible())
		dcSubmissions_Description.forceReflow();

	dcSubmissions_Container.resize(0, 0, 258, 8 + getWord(dcSubmissions_Description.getExtent(), 1));
}

package Client_Dueling {

function disconnectedCleanup(%a)
{
	if ($dcConnected)
	{
		dcClearDuelPane();

		dcAlertMsg.currMsg = "";
		dcMapInfo.currContributer = "";
		dcDuel.currPlayer = "";
		dcBuild.currSave = "";
		dcMaps.currMap = "";
		dcSubmissions.currSubmission = "";
		dcMaps.currWeapon = "";
		dcTestDuel.memberCount = "";
		dcTestDuel.status = "";

		$dcConnected = "";
		dcPlayerFakeList.clear();
		dcPlayerList.deleteAll();
		dcSaveFakeList.clear();
		dcSaveList.deleteAll();
		dcWeaponFakeList.clear();
		dcMapInfo_WeaponList.deleteAll();
		dcWeaponList.deleteAll();
		dcMapFakeList.clear();
		dcMapList.deleteAll();
		dcContributerFakeList.clear();
		dcContributerList.deleteAll();
		dcSubmissionFakeList.clear();
		dcSubmissionList.deleteAll();
		dcSubmissions_Description.deleteAll();
		clientCmddcSetBuilding(0);
		clientCmddcSetChallenging(0);
		clientCmddcSetDueling(0);
		clientCmddcSetBrickCount(0, 0);

		dcDlg.setMode(0);
	}

	parent::disconnectedCleanup(%a);
}

function GuiMLTextCtrl::onUrl(%this, %url)
{
	%a = strReplace(%url, "-", "\t");

	switch$ (getField(%a, 0))
	{
	case "dcStats":
		%bl_id = getField(%a, 1);

		if (!dcDlg.isAwake())
			dcToggleWindow(1);

		dcDlg.setPane(0);
		dcDataClient().getStats(%bl_id);
	case "dcMap":
		%map = getField(%a, 1);
		commandToServer('dcViewMapStats', %map);
	default:
		parent::onUrl(%this, %url);
	}
}

}; // package Client_Dueling

activatePackage(Client_Dueling);

dcGenerateUI();

if ($dcConnected)
	commandToServer('dcRequestTransmission');
