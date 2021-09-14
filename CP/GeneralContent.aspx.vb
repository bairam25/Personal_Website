#Region "Signature"
'################################### Signature ######################################
'############# Date:12-06-2019
'############# Form Name: Settings
'############# Your Name: Ahmed Adel
'################################ End of Signature ##################################

#End Region

#Region "Import"
Imports System.Data
Imports System.Data.SqlClient
Imports BusinessLayer.BusinessLayer
Imports clsMessages
#End Region
Partial Class News
    Inherits System.Web.UI.Page
#Region "Global_Varaibles"
    Dim pf As New PublicFunctions
    Dim UserID As String = "0"
    Dim Phone As String
    Dim Fax As String
    Dim Email As String
    Dim Description As String
    Dim Website As String = String.Empty
    Dim POBox As String = String.Empty
    Dim Address As String = String.Empty
    Dim Youtube As String = String.Empty
    Dim Facebook As String = String.Empty
    Dim Twitter As String = String.Empty
    Dim LinkedIn As String = String.Empty
    Dim Logo As String = String.Empty
    Dim AboutUs As String = String.Empty
    Dim Vision As String = String.Empty
    Dim Mission As String = String.Empty

    Dim _sqlconn As New SqlConnection(DBManager.GetConnectionString)
    Dim _sqltrans As SqlTransaction
#End Region

#Region "Page_Load"
    ''' <summary>
    ''' Handle page load event
    ''' </summary>
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            lblRes.Visible = False
            UserID = PublicFunctions.GetUserId(Page)
            If Not Page.IsPostBack Then
                Permissions.CheckPermisions(New GridView, New LinkButton, New TextBox, New LinkButton, Me.Page, UserID)
                FillData()
            End If
        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
        End Try
    End Sub

#End Region

#Region "Save"
    ''' <summary>
    ''' Save General Content 
    ''' </summary>
    Protected Sub Save(ByVal Sender As Object, ByVal e As System.EventArgs)
        Dim daCompany As New TblCompanyFactory
        Dim dtCompany As New TblCompany
        Try
            If Not FillDT(dtCompany) Then
                Exit Sub
            End If
            _sqlconn.Open()
            _sqltrans = _sqlconn.BeginTransaction
            'Delete old contacts
            ExecuteQuery.ExecuteAlCommands(_sqltrans, _sqlconn, New SqlCommand("delete from TblCompany"))
            If Not daCompany.InsertTrans(dtCompany, _sqlconn, _sqltrans) Then
                _sqltrans.Rollback()
                clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page)
                Exit Sub
            End If
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.Update, Page)
            _sqltrans.Commit()
            _sqlconn.Close()
        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
        End Try

    End Sub
    ''' <summary>
    ''' Fill TblCompany Object
    ''' </summary>
    Function FillDT(ByVal dtCompany As TblCompany) As Boolean
        Try
            '1 - Contact info
            dtCompany.Telephone = txtPhone.Text
            dtCompany.Fax = txtFax.Text
            dtCompany.Email = txtEmail.Text
            dtCompany.Website = txtWebSite.Text
            dtCompany.POBOX = txtPOBox.Text
            dtCompany.Address = txtAddress.TextValue
            '2 - Social Media 
            dtCompany.Facebook = txtFacebook.Text
            dtCompany.Twitter = txtTwitter.Text
            dtCompany.LinkedIn = txtLinkedIn.Text
            dtCompany.Youtube = txtYoutube.Text
            '3- AboutUS
            dtCompany.AboutUS = txtAboutUs.TextValue
            '4- Vission
            dtCompany.Vision = txtVission.TextValue
            '5- Mission
            dtCompany.Mission = txtMission.TextValue
            '6- Working Area 
            dtCompany.WRKBuildingAbilities = txtWRKBuildingAbilities.TextValue
            dtCompany.WRKEconomicBlocs = txtWRKEconomicBlocs.TextValue
            dtCompany.WRKImproveServices = txtWRKImproveServices.TextValue
            dtCompany.WRKIndustrialDev = txtWRKIndustrialDev.TextValue
            dtCompany.WRKInfraServices = txtWRKInfraServices.TextValue
            dtCompany.WRKInstitutionalDev = txtWRKInstitutionalDev.TextValue
            dtCompany.WRKSafetyHealth = txtWRKSafetyHealth.TextValue
            dtCompany.WRKSocialCons = txtWRKSocialCons.TextValue
            dtCompany.WRKEnviromentalCons = txtWRKEnviromentalCons.TextValue
            '7- Organisational framwork 
            dtCompany.ORGSteering = txtORGSteering.TextValue
            dtCompany.ORGCoordinationOffice = txtORGCoordinationOffice.TextValue
            dtCompany.ORGLocalImplement = txtORGLocalImplement.TextValue
            '8- Citizens
            dtCompany.CitizenComplains = txtCitizenComplains.TextValue
            dtCompany.CitizenDesign = txtCitizenDesign.TextValue
            dtCompany.CitizenEvaluation = txtCitizenEvaluation.TextValue
            dtCompany.CitizenFollow = txtCitizenFollow.TextValue
            dtCompany.CitizenIndustrialEvents = txtCitizenIndustrialEvents.TextValue
            dtCompany.CitizenLocalEvents = txtCitizenLocalEvents.TextValue
            dtCompany.CitizenPlan = txtCitizenPlan.TextValue

            dtCompany.CreatedBy = UserID
            dtCompany.CreatedDate = DateTime.Now
            dtCompany.ModifiedDate = DateTime.Now
            dtCompany.IsDeleted = False
            Return True
        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
            Return False
        End Try
    End Function

#End Region

#Region "Fill Data"
    ''' <summary>
    ''' Handle Update Event
    ''' </summary>
    Protected Sub FillData()
        Try
            Dim dt As DataTable = DBManager.Getdatatable("select * from TblCompany")
            If dt.Rows.Count <> 0 Then
                '1 - Contact info
                txtPhone.Text = dt.Rows(0).Item("Telephone").ToString
                txtFax.Text = dt.Rows(0).Item("Fax").ToString
                txtEmail.Text = dt.Rows(0).Item("Email").ToString
                txtWebSite.Text = dt.Rows(0).Item("Website").ToString
                txtPOBox.Text = dt.Rows(0).Item("POBOX").ToString
                txtAddress.TextValue = dt.Rows(0).Item("Address").ToString
                '2 - Social Media 
                txtFacebook.Text = dt.Rows(0).Item("Facebook").ToString
                txtTwitter.Text = dt.Rows(0).Item("Twitter").ToString
                txtLinkedIn.Text = dt.Rows(0).Item("LinkedIn").ToString
                txtYoutube.Text = dt.Rows(0).Item("Youtube").ToString
                '3- AboutUS
                txtAboutUs.TextValue = dt.Rows(0).Item("AboutUS").ToString
                '4- Vission
                txtVission.TextValue = dt.Rows(0).Item("Vision").ToString
                '5- Mission
                txtMission.TextValue = dt.Rows(0).Item("Mission").ToString
                '6- Working Area 
                txtWRKBuildingAbilities.TextValue = dt.Rows(0).Item("WRKBuildingAbilities").ToString
                txtWRKEconomicBlocs.TextValue = dt.Rows(0).Item("WRKEconomicBlocs").ToString
                txtWRKImproveServices.TextValue = dt.Rows(0).Item("WRKImproveServices").ToString
                txtWRKIndustrialDev.TextValue = dt.Rows(0).Item("WRKIndustrialDev").ToString
                txtWRKInfraServices.TextValue = dt.Rows(0).Item("WRKInfraServices").ToString
                txtWRKInstitutionalDev.TextValue = dt.Rows(0).Item("WRKInstitutionalDev").ToString
                txtWRKSafetyHealth.TextValue = dt.Rows(0).Item("WRKSafetyHealth").ToString
                txtWRKSocialCons.TextValue = dt.Rows(0).Item("WRKSocialCons").ToString
                txtWRKEnviromentalCons.TextValue = dt.Rows(0).Item("WRKEnviromentalCons").ToString
                '7- Organisational framwork 
                txtORGSteering.TextValue = dt.Rows(0).Item("ORGSteering").ToString
                txtORGCoordinationOffice.TextValue = dt.Rows(0).Item("ORGCoordinationOffice").ToString
                txtORGLocalImplement.TextValue = dt.Rows(0).Item("ORGLocalImplement").ToString
                '8- Citizens
                txtCitizenComplains.TextValue = dt.Rows(0).Item("CitizenComplains").ToString
                txtCitizenDesign.TextValue = dt.Rows(0).Item("CitizenDesign").ToString
                txtCitizenEvaluation.TextValue = dt.Rows(0).Item("CitizenEvaluation").ToString
                txtCitizenFollow.TextValue = dt.Rows(0).Item("CitizenFollow").ToString
                txtCitizenIndustrialEvents.TextValue = dt.Rows(0).Item("CitizenIndustrialEvents").ToString
                txtCitizenLocalEvents.TextValue = dt.Rows(0).Item("CitizenLocalEvents").ToString
                txtCitizenPlan.TextValue = dt.Rows(0).Item("CitizenPlan").ToString
            End If
        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
        End Try
    End Sub
#End Region

#Region "Cancel"
    ''' <summary>
    ''' hide and show panels
    ''' </summary>
    Protected Sub Cancel(ByVal Sender As Object, ByVal e As System.EventArgs)
        Try
            FillData()
        Catch ex As Exception
            clsMessages.ShowMessage(lblRes, clsMessages.MessageTypesEnum.ERR, Page, ex)
        End Try
    End Sub
#End Region

End Class
