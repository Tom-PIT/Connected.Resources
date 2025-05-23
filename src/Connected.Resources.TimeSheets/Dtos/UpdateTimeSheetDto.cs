﻿using Connected.Annotations;

namespace Connected.Resources.TimeSheets.Dtos;
internal sealed class UpdateTimeSheetDto : TimeSheetDto, IUpdateTimeSheetDto
{
	[MinValue(1)]
	public int Id { get; set; }
}
