IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__Option1__Questio__0BC6C43E]') AND parent_object_id = OBJECT_ID(N'[dbo].[Option1]'))
ALTER TABLE [dbo].[Option1]  WITH CHECK ADD  CONSTRAINT [FK__Option1__Questio__0BC6C43E] FOREIGN KEY([QuestionID])
REFERENCES [dbo].[Question1] ([QuestionID])
GO
ALTER TABLE [dbo].[Option1] CHECK CONSTRAINT [FK__Option1__Questio__0BC6C43E]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__Answer1__Questio__060DEAE8]') AND parent_object_id = OBJECT_ID(N'[dbo].[Answer1]'))
ALTER TABLE [dbo].[Answer1]  WITH CHECK ADD  CONSTRAINT [FK__Answer1__Questio__060DEAE8] FOREIGN KEY([QuestionID])
REFERENCES [dbo].[Question1] ([QuestionID])
GO
ALTER TABLE [dbo].[Answer1] CHECK CONSTRAINT [FK__Answer1__Questio__060DEAE8]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__Answer3__Questio__1FCDBCEB]') AND parent_object_id = OBJECT_ID(N'[dbo].[Answer3]'))
ALTER TABLE [dbo].[Answer3]  WITH CHECK ADD  CONSTRAINT [FK__Answer3__Questio__1FCDBCEB] FOREIGN KEY([QuestionID])
REFERENCES [dbo].[Question3] ([QuestionID])
GO
ALTER TABLE [dbo].[Answer3] CHECK CONSTRAINT [FK__Answer3__Questio__1FCDBCEB]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__Answer2__Questio__08EA5793]') AND parent_object_id = OBJECT_ID(N'[dbo].[Answer2]'))
ALTER TABLE [dbo].[Answer2]  WITH CHECK ADD  CONSTRAINT [FK__Answer2__Questio__08EA5793] FOREIGN KEY([QuestionID])
REFERENCES [dbo].[Question2] ([QuestionID])
GO
ALTER TABLE [dbo].[Answer2] CHECK CONSTRAINT [FK__Answer2__Questio__08EA5793]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__Option2__Questio__0EA330E9]') AND parent_object_id = OBJECT_ID(N'[dbo].[Option2]'))
ALTER TABLE [dbo].[Option2]  WITH CHECK ADD  CONSTRAINT [FK__Option2__Questio__0EA330E9] FOREIGN KEY([QuestionID])
REFERENCES [dbo].[Question2] ([QuestionID])
GO
ALTER TABLE [dbo].[Option2] CHECK CONSTRAINT [FK__Option2__Questio__0EA330E9]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__Answer4__Questio__2C3393D0]') AND parent_object_id = OBJECT_ID(N'[dbo].[Answer4]'))
ALTER TABLE [dbo].[Answer4]  WITH CHECK ADD  CONSTRAINT [FK__Answer4__Questio__2C3393D0] FOREIGN KEY([QuestionID])
REFERENCES [dbo].[Question4] ([QuestionID])
GO
ALTER TABLE [dbo].[Answer4] CHECK CONSTRAINT [FK__Answer4__Questio__2C3393D0]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK__Answer5__Questio__30F848ED]') AND parent_object_id = OBJECT_ID(N'[dbo].[Answer5]'))
ALTER TABLE [dbo].[Answer5]  WITH CHECK ADD  CONSTRAINT [FK__Answer5__Questio__30F848ED] FOREIGN KEY([QuestionID])
REFERENCES [dbo].[Question5] ([QuestionID])
GO
ALTER TABLE [dbo].[Answer5] CHECK CONSTRAINT [FK__Answer5__Questio__30F848ED]