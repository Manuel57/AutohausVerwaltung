Imports System.Data.SqlClient
Imports System.Configuration

Public Class SqlParameterDemoForm

    Private Sub btnCreateEmployee_Click(ByVal sender As System.Object,
        ByVal e As System.EventArgs) Handles btnCreateEmployee.Click

        Dim sqlConnection As New SqlConnection(ConfigurationManager.ConnectionStrings _
        ("SqlParameterDemo.My.MySettings.ConnectionString").ConnectionString)
        Dim command As New SqlCommand
        Dim returnValue As Object

        ' Gets or sets the Transact-SQL statement, table name or stored procedure to 
        ' execute at the data source.
        command.CommandText = "InsertEmployee"
        ' Gets or sets a value indicating how the CommandText property is 
        ' to be interpreted.
        command.CommandType = CommandType.StoredProcedure
        ' Gets the SqlParameterCollection.
        command.Parameters.Add("@Name", SqlDbType.VarChar).Value = tbName.Text.Trim()
        command.Parameters.Add("@Gender", SqlDbType.VarChar).Value = cmbGender.Text
        command.Parameters.Add("@Salary", SqlDbType.VarChar).Value = tbSalary.Text.Trim()
        ' Gets or sets the SqlConnection used by this instance of the SqlCommand.
        command.Connection = sqlConnection
        sqlConnection.Open()
        ' Executes the query, and returns the first column of the first row in the result
        ' set returned by the query. Additional columns or rows are ignored.
        returnValue = command.ExecuteScalar()
        sqlConnection.Close()

        Me.EmployeesTableAdapter.Fill(Me.SampleDataSet.Employees)
    End Sub

    Private Sub SqlParameterDemoForm_Load(ByVal sender As System.Object,
                                        ByVal e As System.EventArgs) Handles MyBase.Load        
        Me.EmployeesTableAdapter.Fill(Me.SampleDataSet.Employees)
    End Sub
End Class