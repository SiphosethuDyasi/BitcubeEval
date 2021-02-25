<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home_Page.aspx.cs" EnableEventValidation = "false" Inherits="SiphosethuDyasi_Section1.Home_Page" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title> Home Page</title>
	<link rel="stylesheet" href="Style_Sheet.css">
	<script type = "text/javascript" >
		function preventBack(){window.history.forward();}
		setTimeout("preventBack()", 0);
		window.onunload=function(){null};
	</script>

	<style>
		.table {
	border-collapse: collapse;
	border-color:white;
	width: 100%;
	font-size:small;
	}

	.header, .tr {
		padding: 8px;
		text-align: left;
		border-bottom: 1px solid #ddd;
	}

	.tr:hover {
		background-color: #f5f5f5;
	}

	.button {
	background-color: white;
	color: dimgray;
	border: 2px solid white;
	font-family: 'Montserrat', sans-serif;
}

	.button:hover {
		background-color: #e7e7e7;
	}

.text {
	border: none;
	border-bottom: 2px solid red;
	font-family: 'Montserrat', sans-serif;
}

	.text:focus {
		background-color: #e7e7e7;
	}
	</style>

</head>
<body>
    <form id="form1" runat="server">
        <div class="grid-container">
			<div class="grid-left"><h3>Welcome <asp:Label runat="server" ID="lblFullName" /></h3></div>
			<div class="grid-right"> <asp:Button runat="server" ID="btnLogOut" Text="Log Out" OnClick="LogOut" CssClass="button4"/></div>
        </div>
		<hr />
			<p>Below are the degrees that your responcible for. <br /> If you want to view the courses and students associated with each degree, <br /> click on the degree you want to view and the information will display.</p>
		<hr />

		<div class="row">
			<div class="column" style="width:27%;">
				<h3>Degree</h3>
				<p><asp:Label runat="server" ID="lblNumOfDegrees" /></p>
				<br />
				<asp:GridView ID="DegreeGrid" runat="server" CssClass="table" HeaderStyle-CssClass="header" RowStyle-CssClass="tr" DataKeyNames="degreeID" AutoGenerateColumns="false" OnRowDataBound="DegreeGrid_RowDataBound" OnSelectedIndexChanged="DegreeGrid_SelectedIndexChanged">
					<Columns>
						<asp:BoundField DataField="degreeName" HeaderText="Degree Name" />
						<asp:BoundField DataField="degreeDuration" HeaderText="Degree Duration (In Years)"/>
					</Columns>
				</asp:GridView>
			</div>

			<div class="column" style="width:26%;">
					<h3>Course</h3>
				<p><asp:Label runat="server" ID="lblCourseNum" /></p>
				<br />
				<asp:GridView ID="CourseGrid" runat="server"  AutoGenerateColumns="false" CssClass="table" HeaderStyle-CssClass="header" RowStyle-CssClass="tr" ShowFooter="true" OnRowEditing="CourseGrid_RowEditing" OnRowUpdating="CourseGrid_RowUpdating" OnRowCancelingEdit="CourseGrid_RowCancelingEdit" OnRowDeleting="CourseGrid_RowDeleting">
					<Columns>
						<asp:TemplateField Visible="false">
							 <ItemTemplate>
								 <asp:Label runat="server" ID="lblCourseID" Text='<%#Eval("courseID") %>' />
							 </ItemTemplate>
						 </asp:TemplateField>
						<asp:TemplateField HeaderText="Course Name">
							<ItemTemplate>
								<asp:Label runat="server" ID="lblCourseName" Text='<%#Eval("courseName") %>' />
							</ItemTemplate>
							<EditItemTemplate>
								<asp:TextBox runat="server" ID="txtCourseName" Text='<%#Eval("courseName") %>' CssClass="text"/>
							</EditItemTemplate>
						</asp:TemplateField>
						<asp:TemplateField HeaderText="Course Duration (In Months)">
							<ItemTemplate>
								<asp:Label runat="server" ID="lblCourseDuration" Text='<%#Eval("duration") %>' />
							</ItemTemplate>
							<EditItemTemplate>
								<asp:TextBox runat="server" ID="txtCourseDuration" Text='<%#Eval("duration") %>' CssClass="text"/>
							</EditItemTemplate>
						</asp:TemplateField>
						<asp:TemplateField HeaderText="Edit">
							<ItemTemplate>
								<asp:Button runat="server" ID="btnEditCourse" Text="Edit" CommandName="Edit" CssClass="button"/>
								<asp:Button runat="server" ID="btnDeleteCourse" Text="Delete" CommandName="Delete" CssClass="button" />
							</ItemTemplate>
							<EditItemTemplate>
								<asp:Button runat="server" ID="btnUpdateCourse" Text="Update" CommandName="Update" CssClass="button"/>
								<asp:Button runat="server" ID="btnCancelCourse" Text="Cancel" CommandName="Cancel" CssClass="button"/>
							</EditItemTemplate>
							<FooterStyle HorizontalAlign="Right" />
								<FooterTemplate>
								 <asp:Button ID="btnAddCourse" runat="server" Text="Add New Course" OnClick="OpenCourse" CssClass="button" />
							</FooterTemplate>
						</asp:TemplateField>
					</Columns>
				</asp:GridView>

				<div runat="server" id="DivCourse" visible="false">
					<asp:TextBox runat="server" ID="txtCName" Placeholder="Course Name" CssClass="text"/>
					<asp:TextBox runat="server" ID="txtDuration" Placeholder="Course Duration (In Months)" CssClass="text"/>
					<asp:Button runat="server" ID="btnSaveCourse" Text="Save" OnClick="SaveCourse" CssClass="button"/>
				</div>
			</div>


			<div class="column" style="width:42%; overflow-x: scroll;" >
					<h3>Students</h3>
				<p><asp:Label runat="server" ID="lblStudentNum" /></p>
				<br />
				<asp:GridView ID="StudentGrid" runat="server" DataKeyNames="studentID" AutoGenerateColumns="false" ShowHeaderWhenEmpty="True" CssClass="table" HeaderStyle-CssClass="header" RowStyle-CssClass="tr" ShowFooter="true" OnRowEditing="StudentGrid_RowEditing" OnRowUpdating="StudentGrid_RowUpdating" OnRowCancelingEdit="StudentGrid_RowCancelingEdit" OnRowDeleting="StudentGrid_RowDeleting">
					<Columns>
						<asp:TemplateField HeaderText="Student Name">
							<ItemTemplate>
								<asp:Label runat="server" ID="lblStudentName" Text='<%#Eval("studentName") %>' />
							</ItemTemplate>
							<EditItemTemplate>
								<asp:TextBox runat="server" ID="txtStudentName" Text='<%#Eval("studentName") %>' CssClass="text" />
							</EditItemTemplate>
						</asp:TemplateField>
						<asp:TemplateField HeaderText="Student Surname">
							<ItemTemplate>
								<asp:Label runat="server" ID="lblStudentSurname" Text='<%#Eval("studentSurname") %>' />
							</ItemTemplate>
							<EditItemTemplate>
								<asp:TextBox runat="server" ID="txtStudentSurname" Text='<%#Eval("studentSurname") %>' CssClass="text"/>
							</EditItemTemplate>
						</asp:TemplateField>
						<asp:TemplateField HeaderText="Student Email">
							<ItemTemplate>
								<asp:Label runat="server" ID="lblStudentEmail" Text='<%#Eval("studentEmail") %>' />
							</ItemTemplate>
							<EditItemTemplate>
								<asp:TextBox runat="server" ID="txtStudentEmail" Text='<%#Eval("studentEmail") %>' CssClass="text"/>
							</EditItemTemplate>
						</asp:TemplateField>
						<asp:TemplateField HeaderText="Student DOB">
							<ItemTemplate>
								<asp:Label runat="server" ID="lblStudentDOB" Text='<%#Eval("studentDOB") %>' />
							</ItemTemplate>
							<EditItemTemplate>
								<asp:TextBox runat="server" ID="txtStudentDOB" Text='<%#Eval("studentDOB") %>' CssClass="text"/>
							</EditItemTemplate>
						</asp:TemplateField>
						 <asp:TemplateField Visible="false">
							 <ItemTemplate>
								 <asp:Label runat="server" ID="lblStudentID" Text='<%#Eval("studentID") %>' />
							 </ItemTemplate>
						 </asp:TemplateField>
						<asp:TemplateField HeaderText="Edit">
							<ItemTemplate>
								<asp:Button runat="server" ID="btnEdit" Text="Edit" CommandName="Edit" CssClass="button"/>
								<asp:Button runat="server" ID="btnDelete" Text="Delete" CommandName="Delete" CssClass="button"/>
							</ItemTemplate>
							<EditItemTemplate>
								<asp:Button runat="server" ID="btnUpdate" Text="Update" CommandName="Update" CssClass="button"/>
								<asp:Button runat="server" ID="btnCancel" Text="Cancel" CommandName="Cancel" CssClass="button"/>
							</EditItemTemplate>
							<FooterStyle HorizontalAlign="Right" />
								<FooterTemplate>
								 <asp:Button ID="btnAdd" runat="server" Text="Add New Student" OnClick="Open" CssClass="button"/>
							</FooterTemplate>
						</asp:TemplateField>
						
							
						
					</Columns>
					<EmptyDataTemplate>No Records Available</EmptyDataTemplate>
				</asp:GridView>
				<div runat="server" id="addDiv" visible="false">
					<asp:TextBox runat="server" ID="txtName" Placeholder="Student First Name" CssClass="text"/>
					<asp:TextBox runat="server" ID="txtSurname" Placeholder="Student Second Name" CssClass="text" />
					<asp:TextBox runat="server" ID="txtEmail" Placeholder="Student Email" CssClass="text" />
					<asp:TextBox runat="server" ID="txtDOB" Placeholder="Student DOB" CssClass="text" />
					<asp:Button runat="server" ID="btnAddNewStudent" Text="Save" OnClick="Save" CssClass="button"/>
				</div>
				
			</div>
		</div>

    </form>
</body>
</html>
