﻿using Connected.Entities;
using Connected.Notifications;
using Connected.Resources.TimeSheets.Dtos;
using Connected.Services;
using Connected.Storage;

namespace Connected.Resources.TimeSheets.Ops;
internal sealed class Update(IStorageProvider storage, IEventService events, ITimeSheetService timeSheets, ITimeSheetCache cache)
	: ServiceAction<IUpdateTimeSheetDto>
{
	protected override async Task OnInvoke()
	{
		var entity = SetState(await timeSheets.Select(Dto)) as TimeSheet ?? throw new NullReferenceException(Strings.ErrEntityExpected);

		await storage.Open<TimeSheet>().Update(entity.Merge(Dto, State.Default), Dto, async () =>
		{
			await cache.Remove(Dto.Id);

			return SetState(await timeSheets.Select(Dto)) as TimeSheet;
		}, Caller);

		await cache.Refresh(Dto.Id);
		await events.Updated(this, timeSheets, entity.Id);
	}
}
