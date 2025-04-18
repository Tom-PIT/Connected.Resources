﻿using Connected.Annotations;
using Connected.Services;
using System.ComponentModel.DataAnnotations;

namespace Connected.Resources.TimeLogs.Dtos;
internal sealed class InsertTimeLogDto : Dto, IInsertTimeLogDto
{
	[MinValue(1)]
	public int Resource { get; set; }

	[NonDefault]
	public DateTimeOffset Start { get; set; }
	public DateTimeOffset? End { get; set; }

	[Required, MaxLength(1024)]
	public required string Type { get; set; }
}
