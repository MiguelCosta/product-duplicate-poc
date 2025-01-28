-- DUE TO Quick filters and Query Planner
CREATE INDEX IX_Products_ProductionType ON Products(ProductionType);

-- Experiencias
CREATE INDEX IX_DuplicateGroups_Status ON DuplicateGroups(Status);
CREATE INDEX IX_DuplicateGroups_Type ON DuplicateGroups(Type);

-- Query planner
CREATE INDEX IX_DuplicateGroups_Type_AssigneeUserId
ON [dbo].[DuplicateGroups] (
    [Type], 
    [AssigneeUserId]
)
INCLUDE (
    [MatchType], 
    [MatchScore], 
    [CreatedDate], 
    [ModifiedDate], 
    [Status], 
    [Note]
);


-- Chat cenas por causa do sort
DROP INDEX IF EXISTS IX_DuplicateGroups_Type_AssigneeUserId ON [dbo].[DuplicateGroups];

CREATE INDEX IX_DuplicateGroups_Type_AssigneeUserId_CreatedDate
ON [dbo].[DuplicateGroups] (
    [Type], 
    [AssigneeUserId], 
    [CreatedDate] -- Add CreatedDate as the last key column to support ORDER BY
)
INCLUDE (
    [MatchType], 
    [MatchScore], 
    [ModifiedDate], 
    [Status], 
    [Note]
);

-- Para também suportar sort pela [ModifiedDate]
CREATE INDEX IX_DuplicateGroups_ModifiedDate
ON [dbo].[DuplicateGroups] ([ModifiedDate])
INCLUDE (
  [Type], 
  [AssigneeUserId],
  [MatchType],
  [MatchScore],
  [CreatedDate],
  [Status],
  [Note]);