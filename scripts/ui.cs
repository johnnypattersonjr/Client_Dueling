// Copyright (c) Johnny Patterson

exec("./profiles.cs");

function dcGenerateUI()
{
	while (isObject(dcDlg))
		dcDlg.delete();

	new GuiControl(dcDlg)
	{
		profile = "GuiDefaultProfile";
		position = "0 0";
		extent = "640 480";

		new GuiWindowCtrl(dcWindow)
		{
			profile = "dcWindowProfile";
			horizSizing = "center";
			vertSizing = "center";
			position = "5 5";
			extent = "630 470";
			accelerator = "escape";
			command = "canvas.popDialog(dcDlg);";
			closeCommand = "canvas.popDialog(dcDlg);";
			text = "Dueling UI";
			maxLength = "255";
			resizeWidth = "0";
			resizeHeight = "0";
			canMinimize = "0";
			canMaximize = "0";

			new GuiControl()
			{
				position = "64 3";
				extent = "204 25";

				new GuiBitmapCtrl()
				{
					position = "0 0";
					extent = "8 25";
					bitmap = "Add-Ons/Client_Dueling/data/ui/bar-left.png";
				};
				new GuiBitmapCtrl(dcNav)
				{
					position = "8 0";
					extent = "44 25";
					bitmap = "Add-Ons/Client_Dueling/data/ui/bar.png";

					new GuiBitmapButtonCtrl(dcStatsBtn)
					{
						profile = "dcTabButtonProfile";
						position = "4 4";
						extent = "36 18";
						command = "dcDlg.setPane(0);";
						text = "Stats";
						buttonType = "RadioButton";
						bitmap = "Add-Ons/Client_Dueling/data/ui/menuBtn";
					};
					new GuiBitmapButtonCtrl(dcDuelBtn)
					{
						profile = "dcTabButtonProfile";
						position = "44 4";
						extent = "32 18";
						command = "dcDlg.setPane(1);";
						text = "Duel";
						buttonType = "RadioButton";
						bitmap = "Add-Ons/Client_Dueling/data/ui/menuBtn";
					};
					new GuiBitmapButtonCtrl(dcBuildBtn)
					{
						profile = "dcTabButtonProfile";
						position = "80 4";
						extent = "34 18";
						command = "dcDlg.setPane(2);";
						text = "Build";
						buttonType = "RadioButton";
						bitmap = "Add-Ons/Client_Dueling/data/ui/menuBtn";
					};
					new GuiBitmapButtonCtrl(dcMapsBtn)
					{
						profile = "dcTabButtonProfile";
						position = "118 4";
						extent = "38 18";
						command = "dcDlg.setPane(3);";
						text = "Maps";
						buttonType = "RadioButton";
						bitmap = "Add-Ons/Client_Dueling/data/ui/menuBtn";
						mColor = "255 255 255 255";
					};
				};
				new GuiBitmapCtrl(dcNav_end)
				{
					position = "52 0";
					extent = "8 25";
					bitmap = "Add-Ons/Client_Dueling/data/ui/bar-right.png";
				};
			};
			new GuiSwatchCtrl()
			{
				position = "8 38";
				extent = "614 420";
				color = "230 230 230 127";
			};
			new GuiControl(dcStats)
			{
				position = "0 30";
				extent = "630 440";

				new GuiSwatchCtrl()
				{
					position = "8 8";
					extent = "208 208";
					color = "255 255 255 255";
				};
				new GuiControl()
				{
					position = "8 8";
					extent = "200 200";

					new GuiSwatchCtrl()
					{
						position = "0 0";
						extent = "200 200";
						color = "127 127 127 127";
					};
					new GuiSwatchCtrl()
					{
						position = "2 2";
						extent = "196 196";
						color = "255 255 255 127";
					};
					new GuiControl()
					{
						position = "2 2";
						extent = "196 196";

						new GuiObjectView(dcAvatar)
						{
							position = "-40 0";
							extent = "260 390";
							cameraZRot = "0";
							forceFOV = "27";
							lightDirection = "0.721277 0.57735 0.57735";
							lightColor = "1.000000 1.000000 1.000000 1.000000";
							ambientColor = "0.500000 0.500000 0.500000 1.000000";
						};
					};
				};
				new GuiControl()
				{
					position = "216 8";
					extent = "406 30";

					new GuiTextCtrl(dcNameTag)
					{
						profile = "dcLargeTextProfile";
						position = "4 4";
						extent = "398 28";
						text = "\c1??? (1337)";
					};
				};
				new GuiSwatchCtrl()
				{
					position = "220 38";
					extent = "398 222";
					color = "200 200 200 255";

					new GuiScrollCtrl()
					{
						profile = "dcScrollProfile";
						position = "2 2";
						extent = "394 218";
						hScrollBar = "alwaysOff";
						vScrollBar = "alwaysOn";

						new GuiSwatchCtrl(dcStats_Container)
						{
							profile = "GuiDefaultProfile";
							position = "1 1";
							extent = "378 32";
							color = "247 247 247 255";

							new GuiMLTextCtrl(dcStats_Text)
							{
								profile = "dcMediumTextProfile";
								position = "7 7";
								extent = "364 19";
								lineSpacing = "2";
								allowColorChars = "1";
							};
						};
					};
				};
				new GuiTextEditCtrl(dcLookup)
				{
					profile = "dcNumberEditProfile";
					position = "16 230";
					extent = "54 20";
					maxLength = "7";
					historySize = "16";
					altCommand = "dcStats.lookupPlayer();";
				};
				new GuiBitmapButtonCtrl()
				{
					profile = "dcButtonProfile";
					position = "78 224";
					extent = "77 32";
					text = "Lookup";
					buttonType = "PushButton";
					bitmap = "Add-Ons/Client_Dueling/data/ui/btn";
					command = "dcStats.lookupPlayer();";
				};
				new GuiBitmapButtonCtrl()
				{
					profile = "dcButtonProfile";
					position = "163 224";
					extent = "37 32";
					text = "Me";
					buttonType = "PushButton";
					bitmap = "Add-Ons/Client_Dueling/data/ui/btn";
					command = "dcStats.lookupMe();";
				};
				new GuiSwatchCtrl()
				{
					position = "8 264";
					extent = "614 8";
					color = "255 255 255 255";
				};
				new GuiSwatchCtrl()
				{
					position = "208 216";
					extent = "8 48";
					color = "255 255 255 255";
				};
				new GuiTextCtrl()
				{
					profile = "dcLargeTextProfile";
					position = "187 275";
					extent = "92 28";
					text = "\c1Rankings:";
				};
				new GuiPopUpMenuCtrl(dcStats_WeaponSelectList)
				{
					profile = "dcDropDownMenuProfile";
					position = "284 278";
					extent = "160 24";
					command = "dcStats.selectWeapon();";
				};
				new GuiTextCtrl()
				{
					profile = "dcLargeTextProfile";
					position = "12 282";
					extent = "125 28";
					text = "\c1Wins/Losses";
				};
				new GuiSwatchCtrl()
				{
					position = "12 308";
					extent = "301 116";
					color = "200 200 200 255";

					new GuiTextListCtrl(dcWinLoss_FakeList)
					{
						profile = "GuiTextListProfile";
						position = "0 0";
						extent = "80 2";
						visible = "0";
						sortedAsc = "1";
						sortedBy = "1";
						sortedNumerical = "1";
					};
					new GuiScrollCtrl()
					{
						profile = "dcScrollProfile";
						position = "2 2";
						extent = "297 112";
						hScrollBar = "alwaysOff";
						vScrollBar = "alwaysOn";

						new GuiSwatchCtrl(dcWinLoss_List)
						{
							position = "1 1";
							extent = "281 2";
							color = "247 247 247 255";
						};
					};
				};
				new GuiTextCtrl()
				{
					profile = "dcLargeTextProfile";
					position = "506 282";
					extent = "112 28";
					text = "\c1Kills/Deaths";
				};
				new GuiSwatchCtrl()
				{
					position = "317 308";
					extent = "301 116";
					color = "200 200 200 255";

					new GuiTextListCtrl(dcKillDeath_FakeList)
					{
						profile = "GuiTextListProfile";
						position = "0 0";
						extent = "80 2";
						visible = "0";
						sortedAsc = "1";
						sortedBy = "1";
						sortedNumerical = "1";
					};
					new GuiScrollCtrl()
					{
						profile = "dcScrollProfile";
						position = "2 2";
						extent = "297 112";
						hScrollBar = "alwaysOff";
						vScrollBar = "alwaysOn";

						new GuiSwatchCtrl(dcKillDeath_List)
						{
							position = "1 1";
							extent = "281 2";
							color = "247 247 247 255";
						};
					};
				};
			};
			new GuiControl(dcDuel)
			{
				position = "0 30";
				extent = "630 440";

				new GuiControl()
				{
					position = "8 8";
					extent = "303 30";

					new GuiTextCtrl()
					{
						profile = "dcLargeTextProfile";
						position = "4 4";
						extent = "196 28";
						text = "\c1Boasts & Challenges";
					};
				};
				new GuiSwatchCtrl()
				{
					position = "12 38";
					extent = "295 342";
					color = "200 200 200 255";

					new GuiTextListCtrl(dcChallengeFakeList)
					{
						profile = "GuiTextListProfile";
						position = "0 0";
						extent = "80 2";
						visible = "0";
						sortedAsc = "1";
						sortedBy = "1";
						sortedNumerical = "1";
					};
					new GuiScrollCtrl()
					{
						profile = "dcScrollProfile";
						position = "2 2";
						extent = "291 338";
						hScrollBar = "alwaysOff";
						vScrollBar = "alwaysOn";

						new GuiSwatchCtrl(dcChallengeList)
						{
							position = "0 0";
							extent = "278 2";
							color = "247 247 247 255";
						};
					};
				};
				new GuiBitmapButtonCtrl(dcDuel_AcceptBtn)
				{
					profile = "dcButtonProfile";
					position = "16 388";
					extent = "90 32";
					command = "dcDuel.accept();";
					text = "Accept";
					buttonType = "PushButton";
					bitmap = "Add-Ons/Client_Dueling/data/ui/btn";
				};
				new GuiBitmapButtonCtrl(dcDuel_DeclineBtn)
				{
					profile = "dcButtonProfile";
					position = "114 388";
					extent = "90 32";
					command = "dcDuel.decline();";
					text = "Decline";
					buttonType = "PushButton";
					bitmap = "Add-Ons/Client_Dueling/data/ui/btn";
				};
				new GuiBitmapButtonCtrl(dcDuel_BoastBtn)
				{
					profile = "dcButtonProfile";
					position = "212 388";
					extent = "91 32";
					command = "dcDuel.boast();";
					text = "Boast";
					buttonType = "PushButton";
					bitmap = "Add-Ons/Client_Dueling/data/ui/btn";
				};
				new GuiSwatchCtrl()
				{
					position = "311 8";
					extent = "8 420";
					color = "255 255 255 255";
				};
				new GuiTextCtrl()
				{
					profile = "dcMediumTextProfile";
					position = "325 11";
					extent = "290 28";
					text = "Boasts: \c4Ranked	  \c0Challenges: \c5Practice";
				};
				new GuiSwatchCtrl()
				{
					position = "319 40";
					extent = "303 8";
					color = "255 255 255 255";
				};
				new GuiControl()
				{
					position = "319 48";
					extent = "303 30";

					new GuiTextCtrl()
					{
						profile = "dcLargeTextProfile";
						position = "4 4";
						extent = "125 28";
						text = "\c1Players";
					};
				};
				new GuiSwatchCtrl()
				{
					position = "323 78";
					extent = "295 302";
					color = "200 200 200 255";

					new GuiTextListCtrl(dcPlayerFakeList)
					{
						profile = "GuiTextListProfile";
						position = "0 0";
						extent = "80 2";
						visible = "0";
						sortedAsc = "1";
						sortedBy = "1";
						sortedNumerical = "1";
					};
					new GuiScrollCtrl()
					{
						profile = "dcScrollProfile";
						position = "2 2";
						extent = "291 298";
						hScrollBar = "alwaysOff";
						vScrollBar = "alwaysOn";

						new GuiSwatchCtrl(dcPlayerList)
						{
							position = "0 0";
							extent = "278 2";
							color = "247 247 247 255";
						};
					};
				};
				new GuiBitmapButtonCtrl(dcDuel_ChallengeBtn)
				{
					profile = "dcButtonProfile";
					position = "327 388";
					extent = "140 32";
					command = "dcDuel.challenge();";
					text = "Challenge";
					buttonType = "PushButton";
					bitmap = "Add-Ons/Client_Dueling/data/ui/btn";
				};
				new GuiTextEditCtrl(dcDuel_SearchInput)
				{
					profile = "dcTextEditProfile";
					position = "475 394";
					extent = "139 20";
					command = "dcDuel.search();";
					altCommand = "dcDuel.search(1);";
				};
				new GuiSwatchCtrl(dcChallengeInfo)
				{
					position = "8 8";
					extent = "614 420";
					visible = "0";
					color = "245 245 245 220";

					new GuiWindowCtrl(dcChallengeInfo_Window)
					{
						profile = "dcWindowProfile";
						position = "177 110";
						extent = "256 220";
						text = "Challenge";
						resizeWidth = "0";
						resizeHeight = "0";
						canMove = "0";
						canMinimize = "0";
						canMaximize = "0";
						closeCommand = "dcChallengeInfo.setVisible(0);";

						new GuiSwatchCtrl()
						{
							position = "8 37";
							extent = "240 172";
							color = "230 230 230 127";
						};
						new GuiControl()
						{
							position = "0 29";
							extent = "240 191";

							new GuiTextCtrl()
							{
								profile = "dcMediumTextProfile";
								position = "16 80";
								extent = "38 23";
								text = "Goal:";
							};
							new GuiTextEditSliderCtrl(dcChallengeInfo_Goal)
							{
								profile = "dcTextEditSliderProfile";
								position = "55 80";
								extent = "32 23";
								format = "%0.0f";
								range = "3.000000 9.000000";
								increment = "1";
							};
							new GuiTextCtrl()
							{
								profile = "dcLargeTextProfile";
								position = "12 12";
								extent = "125 28";
								text = "\c1Settings";
							};
							new GuiSwatchCtrl()
							{
								position = "12 38";
								extent = "232 2";
								color = "200 200 200 255";
							};
							new GuiTextCtrl()
							{
								profile = "dcMediumTextProfile";
								position = "16 48";
								extent = "68 24";
								text = "Weapon:";
							};
							new GuiPopUpMenuCtrl(dcChallengeInfo_WeaponSelectList)
							{
								profile = "dcDropDownMenuProfile";
								position = "88 48";
								extent = "148 24";
							};
							new GuiBitmapButtonCtrl(dcChallengeInfo_ConfirmBtn)
							{
								profile = "dcButtonProfile";
								position = "24 140";
								extent = "92 32";
								command = "dcDuel.challenge(1);";
								text = "Confirm";
								buttonType = "PushButton";
								bitmap = "Add-Ons/Client_Dueling/data/ui/btn";
							};
							new GuiBitmapButtonCtrl()
							{
								profile = "dcButtonProfile";
								position = "132 140";
								extent = "92 32";
								command = "dcChallengeInfo.setVisible(0);";
								text = "Cancel";
								buttonType = "PushButton";
								bitmap = "Add-Ons/Client_Dueling/data/ui/btn";
							};
						};
					};
				};
				new GuiSwatchCtrl(dcDuel_Block)
				{
					position = "8 8";
					extent = "614 420";
					visible = "0";
					color = "245 245 245 220";

					new GuiSwatchCtrl()
					{
						position = "117 178";
						extent = "380 32";
						color = "220 220 220 127";

						new GuiTextCtrl()
						{
							profile = "dcLargeTextProfile";
							position = "14 0";
							extent = "352 32";
							text = "\c1You can\'t challenge anyone right now.";
						};
					};
				};
				new GuiSwatchCtrl(dcDuel_Challenging)
				{
					position = "8 8";
					extent = "614 420";
					color = "245 245 245 220";
					visible = "0";

					new GuiSwatchCtrl()
					{
						position = "211 133";
						extent = "192 153";
						color = "255 255 255 255";

						new GuiSwatchCtrl()
						{
							position = "8 8";
							extent = "176 137";
							color = "230 230 230 127";
						};
						new GuiControl()
						{
							position = "8 8";
							extent = "176 32";

							new GuiTextCtrl()
							{
								profile = "dcLargeTextProfile";
								position = "4 4";
								extent = "168 28";
								text = "\c1Current Challenge";
							};
							new GuiSwatchCtrl()
							{
								position = "4 30";
								extent = "168 2";
								color = "200 200 200 255";
							};
						};
						new GuiMLTextCtrl(dcDuel_ChallengeDescription)
						{
							profile = "dcMediumTextProfile";
							position = "12 44";
							extent = "168 57";
							lineSpacing = "2";
							allowColorChars = "1";
							text = "";
						};
						new GuiBitmapButtonCtrl()
						{
							profile = "dcButtonProfile";
							position = "16 105";
							extent = "160 32";
							command = "dcDuel.cancel();";
							text = "Cancel";
							buttonType = "PushButton";
							bitmap = "Add-Ons/Client_Dueling/data/ui/btn";
						};
					};
				};
			};
			new GuiControl(dcBuild)
			{
				position = "0 30";
				extent = "630 440";
				visible = "0";

				new GuiSwatchCtrl()
				{
					position = "311 8";
					extent = "8 420";
					color = "255 255 255 255";
				};
				new GuiControl()
				{
					position = "319 8";
					extent = "303 32";

					new GuiTextCtrl()
					{
						profile = "dcLargeTextProfile";
						position = "4 4";
						extent = "125 28";
						text = "\c1Saves";
					};
					new GuiSwatchCtrl()
					{
						position = "4 30";
						extent = "295 2";
						color = "200 200 200 255";
					};
				};
				new GuiSwatchCtrl()
				{
					position = "8 56";
					extent = "303 8";
					color = "255 255 255 255";
				};
				new GuiSwatchCtrl()
				{
					position = "323 40";
					extent = "295 384";
					color = "200 200 200 255";

					new GuiTextListCtrl(dcSaveFakeList)
					{
						profile = "GuiTextListProfile";
						position = "0 0";
						extent = "128 16";
						visible = "0";
						sortedAsc = "1";
						sortedBy = "1";
						sortedNumerical = "1";
					};
					new GuiScrollCtrl()
					{
						profile = "dcScrollProfile";
						position = "2 0";
						extent = "291 382";
						hScrollBar = "alwaysOff";
						vScrollBar = "alwaysOn";

						new GuiSwatchCtrl(dcSaveList)
						{
							position = "0 0";
							extent = "278 380";
							minExtent = "8 2";
							color = "247 247 247 255";
						};
					};
				};
				new GuiBitmapButtonCtrl(dcBuildSessionBtn)
				{
					profile = "dcButtonProfile";
					position = "16 16";
					extent = "287 32";
					text = "Start Session";
					buttonType = "PushButton";
					bitmap = "Add-Ons/Client_Dueling/data/ui/btn";
					command = "commandToServer(\'dcStartBuilding\');";
				};
				new GuiTextEditCtrl(dcSaveInput)
				{
					profile = "dcTextEditProfile";
					position = "16 78";
					extent = "140 20";
					maxLength = "16";
					historySize = "16";
					altCommand = "dcBuild.saveBuild();";
				};
				new GuiBitmapButtonCtrl(dcBuild_SaveBtn)
				{
					profile = "dcButtonProfile";
					position = "164 72";
					extent = "139 32";
					text = "Save";
					buttonType = "PushButton";
					bitmap = "Add-Ons/Client_Dueling/data/ui/btn";
					command = "dcBuild.saveBuild();";
				};
				new GuiBitmapButtonCtrl(dcBuild_LoadBtn)
				{
					profile = "dcButtonProfile";
					position = "16 112";
					extent = "287 32";
					text = "Load";
					buttonType = "PushButton";
					bitmap = "Add-Ons/Client_Dueling/data/ui/btn";
					command = "dcBuild.load();";
				};
				new GuiBitmapButtonCtrl(dcBuild_DeleteBtn)
				{
					profile = "dcButtonProfile";
					position = "16 152";
					extent = "287 32";
					command = "dcBuild.delete();";
					text = "Delete";
					buttonType = "PushButton";
					bitmap = "Add-Ons/Client_Dueling/data/ui/btn";
				};
				new GuiBitmapButtonCtrl(dcBuild_SubmitBtn)
				{
					profile = "dcButtonProfile";
					position = "16 232";
					extent = "287 32";
					command = "dcBuild.submit();";
					text = "Submit";
					buttonType = "PushButton";
					bitmap = "Add-Ons/Client_Dueling/data/ui/btn";
				};
				new GuiBitmapButtonCtrl(dcBuild_EditInfoBtn)
				{
					profile = "dcButtonProfile";
					position = "16 272";
					extent = "140 32";
					command = "dcBuild.editInfo();";
					text = "Edit Info";
					buttonType = "PushButton";
					bitmap = "Add-Ons/Client_Dueling/data/ui/btn";
				};
				new GuiBitmapButtonCtrl(dcBuild_ViewStatsBtn)
				{
					profile = "dcButtonProfile";
					position = "164 272";
					extent = "139 32";
					command = "dcBuild.viewStats();";
					text = "View Stats";
					buttonType = "PushButton";
					bitmap = "Add-Ons/Client_Dueling/data/ui/btn";
				};
				new GuiBitmapButtonCtrl(dcBuild_TestDuelBtn)
				{
					profile = "dcButtonProfile";
					position = "16 312";
					extent = "287 32";
					command = "dcBuild.testDuel();";
					text = "Start Test Duel";
					buttonType = "PushButton";
					bitmap = "Add-Ons/Client_Dueling/data/ui/btn";
				};
				new GuiBitmapButtonCtrl(dcBuild_ClearBricksBtn)
				{
					profile = "dcButtonProfile";
					position = "16 388";
					extent = "167 32";
					text = "Clear Bricks";
					buttonType = "PushButton";
					bitmap = "Add-Ons/Client_Dueling/data/ui/btn";
					command = "dcBuild.clearBricks();";
				};
				new GuiBitmapButtonCtrl(dcBuild_CenterBricksBtn)
				{
					profile = "dcButtonProfile";
					position = "191 388";
					extent = "112 32";
					text = "Center";
					buttonType = "PushButton";
					bitmap = "Add-Ons/Client_Dueling/data/ui/btn";
					command = "dcBuild.centerBricks();";
				};
				new GuiSwatchCtrl(dcMapInfo)
				{
					position = "8 8";
					extent = "614 420";
					color = "245 245 245 220";

					new GuiWindowCtrl(dcMapInfo_Window)
					{
						profile = "dcWindowProfile";
						position = "57 50";
						extent = "500 332";
						text = "Map Info";
						resizeWidth = "0";
						resizeHeight = "0";
						canMove = "0";
						canMinimize = "0";
						canMaximize = "0";
						closeCommand = "dcMapInfo.setVisible(0);";

						new GuiSwatchCtrl()
						{
							position = "8 38";
							extent = "484 282";
							color = "230 230 230 127";
						};
						new GuiControl()
						{
							position = "0 30";
							extent = "500 302";

							new GuiSwatchCtrl()
							{
								position = "12 38";
								extent = "230 192";
								color = "200 200 200 255";

								new GuiScrollCtrl()
								{
									profile = "dcScrollProfile";
									position = "2 2";
									extent = "226 188";
									hScrollBar = "alwaysOff";
									vScrollBar = "alwaysOn";

									new GuiSwatchCtrl(dcMapInfo_WeaponList)
									{
										position = "0 0";
										extent = "212 0";
										color = "247 247 247 255";
									};
								};
							};
							new GuiSwatchCtrl()
							{
								position = "258 38";
								extent = "230 204";
								color = "200 200 200 255";

								new GuiTextListCtrl(dcContributerFakeList)
								{
									profile = "GuiTextListProfile";
									position = "0 0";
									extent = "80 128";
									visible = "0";
									sortedNumerical = "1";
									sortedAsc = "1";
									sortedBy = "1";
								};
								new GuiScrollCtrl()
								{
									profile = "dcScrollProfile";
									position = "2 2";
									extent = "226 200";
									hScrollBar = "alwaysOff";
									vScrollBar = "alwaysOn";

									new GuiSwatchCtrl(dcContributerList)
									{
										position = "0 0";
										extent = "212 2";
										color = "247 247 247 255";
									};
								};
							};
							new GuiBitmapButtonCtrl(dcMapInfo_SubmitBtn)
							{
								profile = "dcButtonProfile";
								position = "16 250";
								extent = "107 32";
								command = "dcMapInfo.submit();";
								accelerator = "enter";
								text = "Submit";
								buttonType = "PushButton";
								bitmap = "Add-Ons/Client_Dueling/data/ui/btn";
							};
							new GuiBitmapButtonCtrl()
							{
								profile = "dcButtonProfile";
								position = "131 250";
								extent = "107 32";
								command = "dcMapInfo.setVisible(0);";
								text = "Cancel";
								buttonType = "PushButton";
								bitmap = "Add-Ons/Client_Dueling/data/ui/btn";
							};
							new GuiSwatchCtrl()
							{
								position = "246 8";
								extent = "8 282";
								color = "255 255 255 255";
							};
							new GuiSwatchCtrl()
							{
								position = "8 234";
								extent = "238 8";
								color = "255 255 255 255";
							};
							new GuiTextCtrl()
							{
								profile = "dcLargeTextProfile";
								position = "12 12";
								extent = "230 28";
								text = "\c1Weapons";
							};
							new GuiTextCtrl()
							{
								profile = "dcLargeTextProfile";
								position = "258 12";
								extent = "230 28";
								text = "\c1Contributers";
							};
							new GuiBitmapButtonCtrl(dcMapInfo_RemoveContributerBtn)
							{
								profile = "dcButtonProfile";
								position = "452 250";
								extent = "32 32";
								command = "dcMapInfo.setContributer(0);";
								text = "-";
								buttonType = "PushButton";
								bitmap = "Add-Ons/Client_Dueling/data/ui/btn";
							};
							new GuiBitmapButtonCtrl()
							{
								profile = "dcButtonProfile";
								position = "412 250";
								extent = "32 32";
								command = "dcMapInfo.setContributer(1);";
								text = "+";
								buttonType = "PushButton";
								bitmap = "Add-Ons/Client_Dueling/data/ui/btn";
							};
							new GuiTextEditCtrl(dcContributerInput)
							{
								profile = "dcNumberEditProfile";
								position = "262 256";
								extent = "142 20";
								altCommand = "dcBuild.save();";
								maxLength = "16";
								historySize = "16";
							};
						};
					};
				};
				new GuiSwatchCtrl(dcAlertMsg)
				{
					position = "8 8";
					extent = "614 420";
					visible = "0";
					color = "245 245 245 220";

					new GuiWindowCtrl()
					{
						profile = "dcWindowProfile";
						position = "122 87";
						extent = "370 266";
						text = "Revoke Message";
						maxLength = "255";
						resizeWidth = "0";
						resizeHeight = "0";
						canMove = "0";
						canMinimize = "0";
						canMaximize = "0";
						closeCommand = "dcAlertMsg.setVisible(0);";

						new GuiSwatchCtrl()
						{
							position = "8 37";
							extent = "354 218";
							color = "230 230 230 127";
						};
						new GuiControl()
						{
							position = "0 31";
							extent = "370 234";

							new GuiSwatchCtrl()
							{
								position = "12 11";
								extent = "346 165";
								color = "200 200 200 255";

								new GuiScrollCtrl()
								{
									profile = "dcScrollProfile";
									position = "2 2";
									extent = "342 161";
									hScrollBar = "alwaysOff";
									vScrollBar = "alwaysOn";

									new GuiSwatchCtrl(dcAlertMsg_Container)
									{
										position = "0 0";
										extent = "328 159";
										color = "247 247 247 255";

										new GuiMLTextCtrl(dcAlertMsg_Text)
										{
											profile = "dcMediumTextProfile";
											position = "7 4";
											extent = "314 0";
											maxLength = "1023";
										};
									};
								};
							};
							new GuiBitmapButtonCtrl()
							{
								profile = "dcButtonProfile";
								position = "137 184";
								extent = "96 32";
								command = "dcAlertMsg.confirm();";
								text = "Confirm";
								buttonType = "PushButton";
								bitmap = "Add-Ons/Client_Dueling/data/ui/btn";
							};
						};
					};
				};
				new GuiSwatchCtrl(dcTestDuel)
				{
					position = "8 8";
					extent = "614 420";
					visible = "0";
					color = "245 245 245 220";

					new GuiWindowCtrl()
					{
						profile = "dcWindowProfile";
						position = "71 60";
						extent = "472 320";
						text = "Test Duel";
						resizeWidth = "0";
						resizeHeight = "0";
						canMove = "0";
						canMinimize = "0";
						canMaximize = "0";
						closeCommand = "dcTestDuel.setVisible(0);";

						new GuiSwatchCtrl()
						{
							position = "8 37";
							extent = "456 272";
							color = "230 230 230 127";
						};
						new GuiControl()
						{
							position = "0 29";
							extent = "472 291";

							new GuiSwatchCtrl()
							{
								position = "216 8";
								extent = "8 272";
								color = "255 255 255 255";
							};
							new GuiControl()
							{
								position = "8 8";
								extent = "208 32";

								new GuiTextCtrl()
								{
									profile = "dcLargeTextProfile";
									position = "4 4";
									extent = "125 28";
									text = "\c1Players";
								};
							};
							new GuiSwatchCtrl()
							{
								position = "12 38";
								extent = "200 238";
								color = "200 200 200 255";

								new GuiTextListCtrl(dcBuildSessionFakeList)
								{
									profile = "GuiTextListProfile";
									position = "0 0";
									extent = "80 2";
									visible = "0";
									sortedAsc = "1";
									sortedBy = "1";
									sortedNumerical = "1";
								};
								new GuiScrollCtrl()
								{
									profile = "dcScrollProfile";
									position = "2 2";
									extent = "196 234";
									hScrollBar = "alwaysOff";
									vScrollBar = "alwaysOn";

									new GuiSwatchCtrl(dcBuildSessionList)
									{
										position = "0 0";
										extent = "182 232";
										color = "247 247 247 255";
									};
								};
							};
							new GuiTextCtrl()
							{
								profile = "dcLargeTextProfile";
								position = "228 12";
								extent = "125 28";
								text = "\c1Settings";
							};
							new GuiSwatchCtrl()
							{
								position = "228 38";
								extent = "232 2";
								color = "200 200 200 255";
							};
							new GuiTextCtrl()
							{
								profile = "dcMediumTextProfile";
								position = "232 48";
								extent = "68 24";
								text = "Weapon:";
							};
							new GuiPopUpMenuCtrl(dcTestDuel_WeaponSelectList)
							{
								profile = "dcDropDownMenuProfile";
								position = "304 48";
								extent = "152 24";
							};
							new GuiTextCtrl()
							{
								profile = "dcMediumTextProfile";
								position = "232 80";
								extent = "38 23";
								text = "Goal:";
							};
							new GuiTextEditSliderCtrl(dcTestDuel_Goal)
							{
								profile = "dcTextEditSliderProfile";
								position = "271 80";
								extent = "32 23";
								format = "%0.0f";
								range = "3.000000 9.000000";
								increment = "1";
							};
							new GuiBitmapButtonCtrl(dcTestDuel_StartBtn)
							{
								profile = "dcButtonProfile";
								position = "248 240";
								extent = "192 32";
								text = "Start Duel";
								buttonType = "PushButton";
								bitmap = "Add-Ons/Client_Dueling/data/ui/btn";
								command = "dcTestDuel.start();";
							};
						};
					};
				};
			};
			new GuiControl(dcMaps)
			{
				position = "0 30";
				extent = "630 440";
				visible = "0";

				new GuiSwatchCtrl()
				{
					position = "311 8";
					extent = "8 420";
					color = "255 255 255 255";
				};
				new GuiTextCtrl()
				{
					profile = "dcLargeTextProfile";
					position = "12 12";
					extent = "125 28";
					text = "\c1Weapons";
				};
				new GuiSwatchCtrl()
				{
					position = "12 38";
					extent = "295 386";
					color = "200 200 200 255";

					new GuiTextListCtrl(dcWeaponFakeList)
					{
						profile = "GuiTextListProfile";
						position = "0 0";
						extent = "80 128";
						visible = "0";
						sortedNumerical = "1";
						sortedAsc = "1";
						sortedBy = "1";
					};
					new GuiScrollCtrl()
					{
						profile = "dcScrollProfile";
						position = "2 2";
						extent = "291 382";
						hScrollBar = "alwaysOff";
						vScrollBar = "alwaysOn";

						new GuiSwatchCtrl(dcWeaponList)
						{
							position = "0 0";
							extent = "278 380";
							color = "247 247 247 255";
						};
					};
				};
				new GuiTextCtrl()
				{
					profile = "dcLargeTextProfile";
					position = "323 12";
					extent = "125 28";
					text = "\c1Maps";
				};
				new GuiSwatchCtrl()
				{
					position = "323 38";
					extent = "295 286";
					color = "200 200 200 255";

					new GuiTextListCtrl(dcMapFakeList)
					{
						profile = "GuiTextListProfile";
						position = "0 0";
						extent = "80 2";
						visible = "0";
						sortedNumerical = "1";
						sortedAsc = "1";
						sortedBy = "1";
					};
					new GuiScrollCtrl()
					{
						profile = "dcScrollProfile";
						position = "2 2";
						extent = "291 282";
						hScrollBar = "alwaysOff";
						vScrollBar = "alwaysOn";

						new GuiSwatchCtrl(dcMapList)
						{
							position = "0 0";
							extent = "278 280";
							color = "247 247 247 255";
						};
					};
				};
				new GuiSwatchCtrl()
				{
					position = "319 372";
					extent = "303 8";
					color = "255 255 255 255";
				};
				new GuiBitmapButtonCtrl(dcMaps_ViewStatsBtn)
				{
					profile = "dcButtonProfile";
					position = "327 332";
					extent = "287 32";
					command = "dcMaps.viewStats();";
					text = "Statistics";
					buttonType = "PushButton";
					bitmap = "Add-Ons/Client_Dueling/data/ui/btn";
				};
				new GuiBitmapButtonCtrl(dcMaps_RevokeBtn)
				{
					profile = "dcButtonProfile";
					position = "484 332";
					extent = "130 32";
					visible = "0";
					command = "dcMaps.revoke();";
					text = "Revoke";
					buttonType = "PushButton";
					bitmap = "Add-Ons/Client_Dueling/data/ui/btn";
				};
				new GuiBitmapButtonCtrl(dcMaps_LoadBtn)
				{
					profile = "dcButtonProfile";
					position = "327 388";
					extent = "79 32";
					visible = "0";
					command = "dcMaps.load();";
					text = "Load";
					buttonType = "PushButton";
					bitmap = "Add-Ons/Client_Dueling/data/ui/btn";
				};
				new GuiBitmapButtonCtrl(dcMaps_ViewSubmissionsBtn)
				{
					profile = "dcButtonProfile";
					position = "414 388";
					extent = "200 32";
					command = "dcMaps.viewSubmissions();";
					text = "Submissions (0)";
					buttonType = "PushButton";
					bitmap = "Add-Ons/Client_Dueling/data/ui/btn";
				};
				new GuiSwatchCtrl(dcSubmissions)
				{
					position = "8 8";
					extent = "614 420";
					visible = "0";
					color = "245 245 245 220";

					new GuiWindowCtrl()
					{
						profile = "dcWindowProfile";
						position = "57 50";
						extent = "500 332";
						text = "Submissions";
						resizeWidth = "0";
						resizeHeight = "0";
						canMove = "0";
						canMinimize = "0";
						canMaximize = "0";
						closeCommand = "dcSubmissions.setVisible(0);";

						new GuiSwatchCtrl()
						{
							position = "8 38";
							extent = "484 282";
							color = "230 230 230 127";
						};
						new GuiControl()
						{
							position = "0 30";
							extent = "500 302";

							new GuiSwatchCtrl()
							{
								position = "12 38";
								extent = "230 248";
								color = "200 200 200 255";

								new GuiScrollCtrl()
								{
									profile = "dcScrollProfile";
									position = "2 2";
									extent = "226 244";
									hScrollBar = "alwaysOff";
									vScrollBar = "alwaysOn";

									new GuiSwatchCtrl(dcSubmissionList)
									{
										position = "0 0";
										extent = "212 2";
										color = "247 247 247 255";
									};
								};
								new GuiTextListCtrl(dcSubmissionFakeList)
								{
									profile = "GuiTextListProfile";
									position = "0 0";
									extent = "80 2";
									visible = "0";
									sortedNumerical = "1";
									sortedAsc = "1";
									sortedBy = "1";
								};
							};
							new GuiSwatchCtrl()
							{
								position = "258 38";
								extent = "230 192";
								color = "200 200 200 255";

								new GuiScrollCtrl()
								{
									profile = "dcScrollProfile";
									position = "2 2";
									extent = "226 188";
									hScrollBar = "alwaysOff";
									vScrollBar = "alwaysOn";

									new GuiSwatchCtrl(dcSubmissions_Container)
									{
										position = "0 0";
										extent = "258 0";
										color = "247 247 247 255";

										new GuiMLTextCtrl(dcSubmissions_Description)
										{
											profile = "dcMediumTextProfile";
											position = "7 4";
											extent = "244 0";
											maxLength = "1023";
										};
									};
								};
							};
							new GuiBitmapButtonCtrl(dcSubmissions_LoadBtn)
							{
								profile = "dcButtonProfile";
								position = "262 250";
								extent = "78 32";
								command = "dcSubmissions.load();";
								accelerator = "enter";
								text = "Load";
								buttonType = "PushButton";
								bitmap = "Add-Ons/Client_Dueling/data/ui/btn";
							};
							new GuiBitmapButtonCtrl(dcSubmissions_YesBtn)
							{
								profile = "dcButtonProfile";
								position = "348 250";
								extent = "64 32";
								command = "dcSubmissions.yes();";
								text = "Yes";
								buttonType = "PushButton";
								bitmap = "Add-Ons/Client_Dueling/data/ui/btn";
							};
							new GuiBitmapButtonCtrl(dcSubmissions_NoBtn)
							{
								profile = "dcButtonProfile";
								position = "420 250";
								extent = "64 32";
								command = "dcSubmissions.no();";
								text = "No";
								buttonType = "PushButton";
								bitmap = "Add-Ons/Client_Dueling/data/ui/btn";
							};
							new GuiTextCtrl()
							{
								profile = "dcLargeTextProfile";
								position = "258 12";
								extent = "230 28";
								text = "\c1Description";
							};
							new GuiSwatchCtrl()
							{
								position = "246 8";
								extent = "8 282";
								color = "255 255 255 255";
							};
							new GuiSwatchCtrl()
							{
								position = "254 234";
								extent = "238 8";
								color = "255 255 255 255";
							};
							new GuiTextCtrl()
							{
								profile = "dcLargeTextProfile";
								position = "12 12";
								extent = "230 28";
								text = "\c1Submissions";
							};
						};
					};
				};
				new GuiSwatchCtrl(dcDeclineMsg)
				{
					position = "8 8";
					extent = "614 420";
					visible = "0";
					color = "245 245 245 220";

					new GuiWindowCtrl(dcDeclineWindow)
					{
						profile = "dcWindowProfile";
						position = "122 87";
						extent = "370 266";
						text = "Decline Message";
						resizeWidth = "0";
						resizeHeight = "0";
						canMove = "0";
						canMinimize = "0";
						canMaximize = "0";
						closeCommand = "dcDeclineMsg.setVisible(0);";

						new GuiSwatchCtrl()
						{
							position = "8 37";
							extent = "354 218";
							color = "230 230 230 127";
						};
						new GuiControl()
						{
							position = "0 31";
							extent = "370 234";

							new GuiSwatchCtrl()
							{
								position = "12 11";
								extent = "346 165";
								color = "200 200 200 255";

								new GuiScrollCtrl()
								{
									profile = "dcScrollProfile";
									position = "2 2";
									extent = "342 161";
									hScrollBar = "alwaysOff";
									vScrollBar = "alwaysOn";

									new GuiSwatchCtrl(dcDeclineMsg_Container)
									{
										position = "0 0";
										extent = "328 159";
										color = "247 247 247 255";

										new GuiMLTextCtrl(dcDeclineMsg_Preview)
										{
											profile = "dcMediumTextProfile";
											position = "7 4";
											extent = "314 0";
											maxLength = "1023";
										};
									};
								};
							};
							new GuiTextEditCtrl(dcDeclineMsg_Input)
							{
								profile = "dcTextEditProfile";
								position = "16 188";
								extent = "182 24";
							};
							new GuiBitmapButtonCtrl()
							{
								profile = "dcButtonProfile";
								position = "310 184";
								extent = "44 32";
								command = "dcDeclineMsg.ok();";
								text = "Ok";
								buttonType = "PushButton";
								bitmap = "Add-Ons/Client_Dueling/data/ui/btn";
							};
							new GuiBitmapButtonCtrl()
							{
								profile = "dcButtonProfile";
								position = "206 184";
								extent = "96 32";
								command = "dcDeclineMsg.preview();";
								text = "Preview";
								buttonType = "PushButton";
								bitmap = "Add-Ons/Client_Dueling/data/ui/btn";
							};
						};
					};
				};
			};
			new GuiSwatchCtrl(dcMapStats)
			{
				position = "8 30";
				extent = "614 420";
				visible = "0";
				color = "245 245 245 220";

				new GuiWindowCtrl(dcMapStats_Window)
				{
					profile = "dcWindowProfile";
					position = "157 50";
					extent = "300 320";
					text = "Stats";
					maxLength = "255";
					resizeWidth = "0";
					resizeHeight = "0";
					canMove = "0";
					canMinimize = "0";
					canMaximize = "0";
					closeCommand = "dcMapStats.setVisible(0);";

					new GuiSwatchCtrl()
					{
						position = "8 38";
						extent = "284 270";
						color = "230 230 230 127";
					};
					new GuiControl()
					{
						position = "0 30";
						extent = "300 290";

						new GuiSwatchCtrl()
						{
							position = "12 12";
							extent = "276 262";
							color = "200 200 200 255";

							new GuiScrollCtrl()
							{
								profile = "dcScrollProfile";
								position = "2 2";
								extent = "272 258";
								hScrollBar = "alwaysOff";
								vScrollBar = "alwaysOn";

								new GuiSwatchCtrl(dcMapStats_Container)
								{
									position = "0 0";
									extent = "258 0";
									color = "247 247 247 255";

									new GuiMLTextCtrl()
									{
										profile = "dcMediumTextProfile";
										position = "7 4";
										extent = "244 20";
										text = "\c1Rating";
									};
									new GuiSwatchCtrl()
									{
										position = "8 24";
										extent = "242 16";
										color = "200 200 200 255";

										new GuiSwatchCtrl()
										{
											position = "2 2";
											extent = "238 12";
											color = "255 190 190 255";

											new GuiSwatchCtrl(dcMapStats_Rating)
											{
												position = "0 0";
												extent = "8 12";
												color = "185 245 185 255";
												minExtent = "0 0";
											};
										};
									};
									new GuiMLTextCtrl(dcMapStats_Text)
									{
										profile = "dcMediumTextProfile";
										position = "7 41";
										extent = "244 0";
										maxLength = "1023";
									};
								};
							};
						};
					};
				};
			};
			new GuiSwatchCtrl(dcCheckDataServer)
			{
				position = "8 38";
				extent = "614 420";
				visible = "0";
				color = "245 245 245 220";

				new GuiSwatchCtrl()
				{
					position = "147 178";
					extent = "320 32";
					color = "220 220 220 127";

					new GuiTextCtrl()
					{
						profile = "dcLargeTextProfile";
						position = "48 0";
						extent = "224 32";
						text = "\c1Checking Data Server...";
					};
				};
			};
			new GuiSwatchCtrl(dcNoDataServer)
			{
				position = "8 38";
				extent = "614 420";
				visible = "0";
				color = "245 245 245 220";

				new GuiSwatchCtrl()
				{
					position = "147 178";
					extent = "320 32";
					color = "220 220 220 127";

					new GuiTextCtrl()
					{
						profile = "dcLargeTextProfile";
						position = "12 0";
						extent = "296 32";
						text = "\c1Cannot Connect to Data Server";
					};
				};
				new GuiBitmapButtonCtrl()
				{
					profile = "dcButtonProfile";
					position = "267 218";
					extent = "80 32";
					command = "dcDataClient.retryPing();";
					text = "Retry";
					buttonType = "PushButton";
					bitmap = "Add-Ons/Client_Dueling/data/ui/btn";
					accelerator = "enter";
				};
			};
			new GuiSwatchCtrl(dcPrompt)
			{
				position = "8 38";
				extent = "614 420";
				visible = "0";
				color = "245 245 245 220";

				new GuiSwatchCtrl()
				{
					position = "147 178";
					extent = "320 32";
					color = "220 220 220 127";

					new GuiTextCtrl(dcPrompt_Text)
					{
						profile = "dcLargeCenterTextProfile";
						position = "0 0";
						extent = "320 32";
					};
				};
				new GuiBitmapButtonCtrl(dcPrompt_Yes)
				{
					profile = "dcButtonProfile";
					position = "223 218";
					extent = "80 32";
					visible = "0";
					text = "Yes";
					buttonType = "PushButton";
					bitmap = "Add-Ons/Client_Dueling/data/ui/btn";
					command = "dcPrompt.cmd(Yes);";
				};
				new GuiBitmapButtonCtrl(dcPrompt_No)
				{
					profile = "dcButtonProfile";
					position = "311 218";
					extent = "80 32";
					visible = "0";
					text = "No";
					buttonType = "PushButton";
					bitmap = "Add-Ons/Client_Dueling/data/ui/btn";
					command = "dcPrompt.cmd(No);";
				};
				new GuiBitmapButtonCtrl(dcPrompt_Good)
				{
					profile = "dcButtonProfile";
					position = "179 218";
					extent = "80 32";
					visible = "0";
					text = "Good";
					buttonType = "PushButton";
					bitmap = "Add-Ons/Client_Dueling/data/ui/btn";
					command = "dcPrompt.cmd(Good);";
				};
				new GuiBitmapButtonCtrl(dcPrompt_Ok)
				{
					profile = "dcButtonProfile";
					position = "267 218";
					extent = "80 32";
					visible = "0";
					text = "Ok";
					buttonType = "PushButton";
					bitmap = "Add-Ons/Client_Dueling/data/ui/btn";
					command = "dcPrompt.cmd(Ok);";
				};
				new GuiBitmapButtonCtrl(dcPrompt_Bad)
				{
					profile = "dcButtonProfile";
					position = "355 218";
					extent = "80 32";
					visible = "0";
					text = "Bad";
					buttonType = "PushButton";
					bitmap = "Add-Ons/Client_Dueling/data/ui/btn";
					command = "dcPrompt.cmd(Bad);";
				};
			};
		};
	};

	while (isObject(dcBrickCount))
		dcBrickCount.delete();

	new GuiMLTextCtrl(dcBrickCount)
	{
		profile = "GuiDefaultProfile";
		horizSizing = "center";
		vertSizing = "relative";
		profile = "BlockChatTextSize4Profile";
		position = "0 4";
		extent = "192 24";
		visible = "0";
		text = "<just:center><color:ffffff>Bricks: 0/0 ";
	};

	while (isObject(dcDataClient))
		dcDataClient.delete();

	PlayGui.add(dcBrickCount);
	%w = getWord(canvas.getExtent(), 0);
	dcBrickCount.resize(0, 4, %w, 32);

	// Blockland must load Avatar data before we can display a Blockhead with special attributes.
	AvatarGui.onWake();
	dcAvatar.setObject("", "base/data/shapes/player/m.dts", "", 100);

	dcChallengeInfo_Goal.setValue(3);
	dcTestDuel_Goal.setValue(3);
	dcStatsBtn.setValue(1);

	dcDlg.paneCount = 4;
	dcDlg.pane[0] = nameToID(dcStats);
	dcDlg.pane[1] = nameToID(dcDuel);
	dcDlg.pane[2] = nameToID(dcBuild);
	dcDlg.pane[3] = nameToID(dcMaps);

	dcDlg.btn[0] = nameToID(dcStatsBtn);
	dcDlg.btn[1] = nameToID(dcDuelBtn);
	dcDlg.btn[2] = nameToID(dcBuildBtn);
	dcDlg.btn[3] = nameToID(dcMapsBtn);
}

function dcAvatar::onWake(%this)
{
	%pi = mAtan(1, 1) * 4;
	%this.schedule(0, setCameraRot, %pi / 11, 0, 51 * %pi / 48);
	%this.schedule(0, setOrbitDist, 5);

	if (%this.loaded)
		return;

	dcAvatarReset(%this);
	dcAvatarUpdate(%this);
	%this.loaded = 1;
}

function dcDuelist::onWake(%this)
{
	%pi = mAtan(1, 1) * 4;
	%this.schedule(0, setCameraRot, %pi / 11, 0, 51 * %pi / 48);
	%this.schedule(0, setOrbitDist, 5);

	if (%this.loaded)
		return;

	dcAvatarReset(%this);
	dcAvatarUpdate(%this);
	%this.loaded = 1;
}

function dcAvatarReset(%this)
{
	%this.aDecalName = "AAA-None";
	%this.aFaceName = "smiley";

	%this.aAccentColor = "1 1 1 1";
	%this.aChestColor = "0.900 0.900 0.900 1.000";
	%this.aHatColor = "1 1 1 1";
	%this.aHeadColor = "1 0.878431 0.611765 1";
	%this.aHipColor = "0.200 0.000 0.800 1.000";
	%this.aLArmColor = "0.900 0.000 0.000 1.000";
	%this.aLHandColor = "1 0.878431 0.611765 1";
	%this.aLLegColor = "0.200 0.000 0.800 1.000";
	%this.aPackColor = "1 1 1 1";
	%this.aRArmColor = "0.900 0.000 0.000 1.000";
	%this.aRHandcolor = "1 0.878431 0.611765 1";
	%this.aRLegcolor = "0.200 0.000 0.800 1.000";
	%this.aSecondPackColor = "1 1 1 1";

	%this.aAccent = 0;
	%this.aChest = 0;
	%this.aHat = 0;
	%this.aHip = 0;
	%this.aLArm = 0;
	%this.aLHand = 0;
	%this.aLLeg = 0;
	%this.aPack = 0;
	%this.aRArm = 0;
	%this.aRHand = 0;
	%this.aRLeg = 0;
	%this.aSecondPack = 0;
}

function dcAvatarUpdate(%this, %headOnly, %flip)
{
	for (%a = 0; %a < $numDecal; %a++)
		if (fileBase($decal[%a]) $= fileBase(%this.aDecalName))
			break;

	if (%a >= $numDecal)
		%a = 0;

	%this.setIflFrame("", "decal", %a);

	for (%a = 0; %a < $numFace; %a++)
		if (fileBase($face[%a]) $= fileBase(%this.aFaceName))
			break;

	if (%a >= $numFace)
		%a = 0;

	%this.setIflFrame("", "face", %a);

	%accentColor = %this.aAccentColor;
	%chestColor = %this.aChestColor;
	%hatColor = %this.aHatColor;
	%headColor = %this.aHeadColor;
	%hipColor = %this.aHipColor;
	%larmColor = %this.aLArmColor;
	%lhandColor = %this.aLHandColor;
	%llegColor = %this.aLLegColor;
	%packColor = %this.aPackColor;
	%rarmColor = %this.aRArmColor;
	%rhandcolor = %this.aRHandColor;
	%rlegcolor = %this.aRLegColor;
	%secondPackColor = %this.aSecondPackColor;

	%accent = %this.aAccent;
	%chest = %this.aChest;
	%hat = %this.aHat;
	%hip = %this.aHip;
	%larm = %this.aLArm;
	%lhand = %this.aLHand;
	%lleg = %this.aLLeg;
	%pack = %this.aPack;
	%rarm = %this.aRArm;
	%rhand = %this.aRHand;
	%rleg = %this.aRLeg;
	%secondPack = %this.aSecondPack;

	if (%hat == 1 && %accent)
		%accent = 4;
	else if (%hat != 4 && %hat != 6 && %hat != 7)
		%accent = 0;

	%c = -1;
	%n[%c++] = "armor";
	%n[%c++] = "bicorn";
	%n[%c++] = "bucket";
	%n[%c++] = "cape";
	%n[%c++] = "chest";
	%n[%c++] = "copHat";
	%n[%c++] = "femchest";
	%n[%c++] = "flareHelmet";
	%n[%c++] = "epaulets";
	%n[%c++] = "epauletsRankA";
	%n[%c++] = "epauletsRankB";
	%n[%c++] = "epauletsRankC";
	%n[%c++] = "epauletsRankD";
	%n[%c++] = "helmet";
	%n[%c++] = "knitHat";
	%n[%c++] = "lArm";
	%n[%c++] = "lArmSlim";
	%n[%c++] = "lHand";
	%n[%c++] = "lHook";
	%n[%c++] = "lPeg";
	%n[%c++] = "lShoe";
	%n[%c++] = "lSki";
	%n[%c++] = "pack";
	%n[%c++] = "pants";
	%n[%c++] = "plume";
	%n[%c++] = "pointyHelmet";
	%n[%c++] = "quiver";
	%n[%c++] = "rArm";
	%n[%c++] = "rArmSlim";
	%n[%c++] = "rHand";
	%n[%c++] = "rHook";
	%n[%c++] = "rPeg";
	%n[%c++] = "rShoe";
	%n[%c++] = "rSki";
	%n[%c++] = "scoutHat";
	%n[%c++] = "septPlume";
	%n[%c++] = "shoulderPads";
	%n[%c++] = "skirtHip";
	%n[%c++] = "skirtTrimLeft";
	%n[%c++] = "skirtTrimRight";
	%n[%c++] = "tank";
	%n[%c++] = "triPlume";
	%n[%c++] = "visor";
	%c++;

	for (%a = 0; %a < %c; %a++)
		%this.hideNode("", %n[%a]);

	%accent[%numAccent++] = "plume";
	%accent[%numAccent++] = "triPlume";
	%accent[%numAccent++] = "septPlume";
	%accent[%numAccent++] = "visor";
	%numAccent++;

	%numChest = -1;
	%chest[%numChest++] = "chest";
	%chest[%numChest++] = "femChest";
	%numChest++;

	%hat[%numHat++] = "helmet";
	%hat[%numHat++] = "pointyHelmet";
	%hat[%numHat++] = "flareHelmet";
	%hat[%numHat++] = "scoutHat";
	%hat[%numHat++] = "bicorn";
	%hat[%numHat++] = "copHat";
	%hat[%numHat++] = "knitHat";
	%numHat++;

	%numHip = -1;
	%hip[%numHip++] = "pants";
	%hip[%numHip++] = "skirtHip";
	%numHip++;

	%numLArm = -1;
	%larm[%numLArm++] = "lArm";
	%larm[%numLArm++] = "lArmSlim";
	%numLArm++;

	%numLHand = -1;
	%lhand[%numLHand++] = "lHand";
	%lhand[%numLHand++] = "lHook";
	%numLHand++;

	%numLLeg = -1;
	%lleg[%numLLeg++] = "lShoe";
	%lleg[%numLLeg++] = "lPeg";
	%numLLeg++;

	%pack[%numPack++] = "armor";
	%pack[%numPack++] = "bucket";
	%pack[%numPack++] = "cape";
	%pack[%numPack++] = "pack";
	%pack[%numPack++] = "quiver";
	%pack[%numPack++] = "tank";
	%numPack++;

	%numRArm = -1;
	%rarm[%numRArm++] = "rArm";
	%rarm[%numRArm++] = "rArmSlim";
	%numRArm++;

	%numRHand = -1;
	%rhand[%numRHand++] = "rHand";
	%rhand[%numRHand++] = "rHook";
	%numRHand++;

	%numRLeg = -1;
	%rleg[%numRLeg++] = "rShoe";
	%rleg[%numRLeg++] = "rPeg";
	%numRLeg++;

	%secondPack[%numSecondPack++] = "epaulets";
	%secondPack[%numSecondPack++] = "epauletsRankA";
	%secondPack[%numSecondPack++] = "epauletsRankB";
	%secondPack[%numSecondPack++] = "epauletsRankC";
	%secondPack[%numSecondPack++] = "epauletsRankD";
	%secondPack[%numSecondPack++] = "shoulderPads";
	%numSecondPack++;

	%this.setSequence("", 0, "headup", %secondPack || %pack);

	if (!%headOnly)
	{
		%this.unHideNode("", %chest[%chest]);
		%this.unHideNode("", %larm[%larm]);
		%this.unHideNode("", %lhand[%lhand]);
		%this.unHideNode("", %lleg[%lleg]);
		%this.unHideNode("", %hip[%hip]);
		%this.unHideNode("", %rarm[%rarm]);
		%this.unHideNode("", %rhand[%rhand]);
		%this.unHideNode("", %rleg[%rleg]);

		if (%pack[%pack] !$= "")
			%this.unHideNode("", %pack[%pack]);

		if (%secondPack[%secondPack] !$= "")
			%this.unHideNode("", %secondPack[%secondPack]);
	}

	if (%accent[%accent] !$= "" && %hat)
		%this.unHideNode("", %accent[%accent]);

	if (%hat[%hat] !$= "")
		%this.unHideNode("", %hat[%hat]);

	%this.unHideNode("", "headSkin");

	if (!%headOnly)
	{
		for (%a = 0; %a < %numChest; %a++)
			%this.setNodeColor("", %chest[%a], %a == %chest ? %chestColor : "1 1 1 1");

		for (%a = 0; %a < %numLArm; %a++)
			%this.setNodeColor("", %larm[%a], %a == %larm ? %larmColor : "1 1 1 1");

		for (%a = 0; %a < %numLHand; %a++)
			%this.setNodeColor("", %lhand[%a], %a == %lhand ? %lhandColor : "1 1 1 1");

		for (%a = 0; %a < %numHip; %a++)
			%this.setNodeColor("", %hip[%a], %a == %hip ? %hipColor : "1 1 1 1");

		for (%a = 0; %a < %numLLeg; %a++)
			%this.setNodeColor("", %lleg[%a], %a == %lleg ? %llegColor : "1 1 1 1");

		for (%a = 0; %a < %numRArm; %a++)
			%this.setNodeColor("", %rarm[%a], %a == %rarm ? %rarmColor : "1 1 1 1");

		for (%a = 0; %a < %numRHand; %a++)
			%this.setNodeColor("", %rhand[%a], %a == %rhand ? %rhandColor : "1 1 1 1");

		for (%a = 0; %a < %numRLeg; %a++)
			%this.setNodeColor("", %rleg[%a], %a == %rleg ? %rlegColor : "1 1 1 1");

		for (%a = 1; %a < %numPack; %a++)
			%this.setNodeColor("", %pack[%a], %a == %pack ? %packColor : "1 1 1 1");

		for (%a = 1; %a < %numSecondPack; %a++)
			%this.setNodeColor("", %secondPack[%a], %a == %secondPack ? %secondPackColor : "1 1 1 1");
	}

	for (%a = 1; %a < %numAccent; %a++)
		%this.setNodeColor("", %accent[%a], %a == %accent ? %accentColor : "1 1 1 1");

	for (%a = 1; %a < %numHat; %a++)
		%this.setNodeColor("", %hat[%a], %a == %hat ? %hatColor : "1 1 1 1");

	%this.setNodeColor("", "headSkin", %headColor);

	%pi = mAtan(1, 1) * 4;
	%flip = %flip ? -1 : 1;
	%this.schedule(0, setCameraRot, %pi / 11, 0, %flip * 51 * %pi / 48);
	%this.schedule(0, setOrbitDist, 5);
}

function dcDlg::onWake(%this)
{
	%this.setMode(%this.currMode);

	if (!%this.weaponsLoaded)
		dcDataClient().getWeapons();

	if (!strLen(%this.currPane))
		%this.setPane(0);
	else
		%this.setPane(%this.currPane);
}

function dcDlg::onSleep(%this)
{
	if (%this.currPane == 1)
	{
		if ($dcConnected)
			commandToServer('dcCloseDuelPane');

		dcClearDuelPane();
	}
}

function dcClearDuelPane()
{
	%list = nameToID(dcPlayerFakeList);
	if (!isObject(%list))
		return;

	%count = %list.rowCount();

	for (%i = 0; %i < %count; %i++)
	{
		%id = %list.getRowId(%i);

		%btn = dcDuel.playerBtn[%id];
		%tab = dcDuel.playerTab[%id];
		%icon = dcDuel.playerIcon[%id];

		if (isObject(%tab))
			%tab.delete();
		if (isObject(%btn))
			%btn.delete();
		if (isObject(%icon))
			%icon.delete();

		dcDuel.playerBtn[%id] = "";
		dcDuel.playerIcon[%id] = "";
		dcDuel.playerStatus[%id] = "";
		dcDuel.playerTab[%id] = "";
	}

	%list.clear();

	dcDuel.selectPlayer();
	dcChallengeInfo.setVisible(0);
}
