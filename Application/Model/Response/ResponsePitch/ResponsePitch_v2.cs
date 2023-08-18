﻿using Application.Model.Response.ResponseSchedule;

namespace Application.Model.Response.ResponsePitch;

public class ResponsePitchV2
{
    public Guid PitchId { get; set; }
    public string Name { get; set; } = null!;
    public string Size { get; set; } = null!;
    public Guid LandId { get; set; }

    public List<ResponseSchedule_v2>? Schedules { get; set; }
}