﻿using System;

namespace Vulcanova.Features.Notes;

public class Note
{
    public int Id { get; set; }
    public Guid Key { get; set; }
    public int PupilId { get; set; }
    public int AccountId { get; set; }
    public string CreatorName { get; set; }
    public string Content { get; set; }
    public object Points { get; set; }
    public string CategoryName { get; set; }
    public object CategoryType { get; set; }
    public bool Positive { get; set; }
    public DateTime DateModified { get; set; }
}