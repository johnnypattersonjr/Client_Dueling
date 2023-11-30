// Copyright (c) Johnny Patterson

$dcProfilesLoaded = 0;

if (!$dcProfilesLoaded)
{
	$dcProfilesLoaded = 1;

	while (isObject(dcDefaultProfile))
		dcDefaultProfile.delete();

	new GuiControlProfile(dcDefaultProfile)
	{
		tab = "0";
		canKeyFocus = "0";
		mouseOverSelected = "0";
		modal = "1";
		opaque = "0";
		fillColor = "200 200 200 255";
		fillColorHL = "200 200 200 255";
		fillColorNA = "200 200 200 255";
		border = "0";
		borderThickness = "1";
		borderColor = "0 0 0 255";
		borderColorHL = "128 128 128 255";
		borderColorNA = "64 64 64 255";
		fontType = "Arial";
		fontSize = "16";
		fontColors[0] = "0 0 0 255";
		fontColors[1] = "32 100 100 255";
		fontColors[2] = "0 0 0 255";
		fontColors[3] = "200 200 200 255";
		fontColors[4] = "0 0 204 255";
		fontColors[5] = "85 26 139 255";
		fontColors[6] = "0 0 0 0";
		fontColors[7] = "0 0 0 0";
		fontColors[8] = "0 0 0 0";
		fontColors[9] = "0 0 0 0";
		fontColor = "69 69 69 255";
		fontColorHL = "128 128 128 255";
		fontColorNA = "200 200 200 255";
		fontColorSEL = "200 200 200 255";
		fontColorLink = "0 0 204 255";
		fontColorLinkHL = "85 26 139 255";
		doFontOutline = "0";
		fontOutlineColor = "255 255 255 255";
		justify = "left";
		textOffset = "0 0";
		autoSizeWidth = "0";
		autoSizeHeight = "0";
		returnTab = "0";
		numbersOnly = "0";
		cursorColor = "0 0 0 255";
		bitmap = "base/client/ui/BlockWindow";
		hasBitmapArray = "0";
	};

	while (isObject(dcWindowProfile))
		dcWindowProfile.delete();

	new GuiControlProfile(dcWindowProfile : dcDefaultProfile)
	{
		opaque = "1";
		fillColor = "255 255 255 255";
		fillColorHL = "171 171 171 255";
		fillColorNA = "171 171 171 255";
		border = "2";
		borderThickness = "1";
		borderColor = "0 0 0 255";
		borderColorHL = "128 128 128 255";
		borderColorNA = "64 64 64 255";
		fontType = "Impact";
		fontSize = "18";
		fontColor = "255 255 255 255";
		textOffset = "5 4";
		bitmap = "Add-Ons/Client_Dueling/data/ui/window.png";
		hasBitmapArray = "1";
		text = "Window";
	};

	while (isObject(dcButtonProfile))
		dcButtonProfile.delete();

	new GuiControlProfile(dcButtonProfile : dcDefaultProfile)
	{
		opaque = "1";
		fillColor = "255 255 255 255";
		fillColorHL = "0 0 0 255";
		fillColorNA = "221 202 173 255";
		fontSize = "24";
		justify = "center";
	};

	while (isObject(dcTabButtonProfile))
		dcTabButtonProfile.delete();

	new GuiControlProfile(dcTabButtonProfile : dcDefaultProfile)
	{
		opaque = "1";
		fillColor = "255 255 255 255";
		fillColorHL = "0 0 0 255";
		fillColorNA = "221 202 173 255";
		justify = "center";
	};

	while (isObject(dcMediumTextProfile))
		dcMediumTextProfile.delete();

	new GuiControlProfile(dcMediumTextProfile : dcDefaultProfile)
	{
		fontSize = "20";
	};

	while (isObject(dcLargeTextProfile))
		dcLargeTextProfile.delete();

	new GuiControlProfile(dcLargeTextProfile : dcDefaultProfile)
	{
		fontSize = "24";
	};

	while (isObject(dcLargeCenterTextProfile))
		dcLargeCenterTextProfile.delete();

	new GuiControlProfile(dcLargeCenterTextProfile : dcLargeTextProfile)
	{
		justify = "center";
	};

	while (isObject(dcScrollProfile))
		dcScrollProfile.delete();

	new GuiControlProfile(dcScrollProfile : dcDefaultProfile)
	{
		opaque = "1";
		fillColor = "247 247 247 255";
		bitmap = "Add-Ons/Client_Dueling/data/ui/scroll";
		hasBitmapArray = "1";
	};

	while (isObject(dcRadioProfile))
		dcRadioProfile.delete();

	new GuiControlProfile(dcRadioProfile : dcDefaultProfile)
	{
		bitmap = "Add-Ons/Client_Dueling/data/ui/radio";
		fixedExtent = "1";
		hasBitmapArray = "1";
	};

	while (isObject(dcTextEditSliderProfile))
		dcTextEditSliderProfile.delete();

	new GuiControlProfile(dcTextEditSliderProfile : dcDefaultProfile)
	{
		fontSize = "20";
		textOffset = "0 2";
	};

	while (isObject(dcDropDownMenuProfile))
		dcDropDownMenuProfile.delete();

	new GuiControlProfile(dcDropDownMenuProfile : dcDefaultProfile)
	{
		opaque = "1";
		fillColor = "200 200 200 255";
		fillColorHL = "180 180 180 255";
		border = "1";
		borderThickness = "1";
		borderColor = "69 69 69 255";
		fontSize = "22";
		justify = "center";
	};

	while (isObject(dcTextEditProfile))
		dcTextEditProfile.delete();

	new GuiControlProfile(dcTextEditProfile : dcDefaultProfile)
	{
		canKeyFocus = "1";
		opaque = "1";
		fillColor = "247 247 247 255";
		border = "1";
		borderThickness = "1";
		borderColor = "127 127 127 255";
		fontSize = "16";
		justify = "left";
		textOffset = "0 2";
	};

	while (isObject(dcNumberEditProfile))
		dcNumberEditProfile.delete();

	new GuiControlProfile(dcNumberEditProfile : dcTextEditProfile)
	{
		numbersOnly = "1";
	};
}
