

Public Class clsMessages
#Region "New Way"
    Private Const InsertMsg As String = "تم إضافة السجل بنجاح"
    Private Const ErrorMsg As String = "حدث خطأ..الرجاء الاتصال بفريق الدعم"
    Private Const UpdateMSG As String = "تم تعديل السجل بنجاح"
    Private Const DeleteMSG As String = "تم حذف السجل بنجاح"
    Private Const INFOMSG As String = "القيمة المدخلة موجودة قبل ذلك...الرجاء إدخال قيمة أخرى"

    Enum MessageTypesEnum
        Insert
        Update
        Delete
        CUSTOMInfo
        CUSTOMSuccess
        CustomErr
        INFO
        ERR
    End Enum


    Public Shared Sub ShowMessage(ByRef lblRes As Label, ByVal messageType As MessageTypesEnum, ByVal page As Page, Optional EX As Exception = Nothing, Optional ByVal DetailedMSG As String = "This Value alredy Exists")
        Try
            Select Case messageType

                Case MessageTypesEnum.Insert
                    ShowSuccessMessgage(lblRes, InsertMsg, page)
                Case MessageTypesEnum.Update
                    ShowSuccessMessgage(lblRes, UpdateMSG, page)
                Case MessageTypesEnum.Delete
                    ShowSuccessMessgage(lblRes, DeleteMSG, page)
                Case MessageTypesEnum.INFO
                    ShowInfoMessgage(lblRes, INFOMSG, page)
                Case MessageTypesEnum.CUSTOMInfo
                    ShowCustomMessgage(lblRes, DetailedMSG, page)
                Case MessageTypesEnum.CUSTOMSuccess
                    ShowSuccessMessgage(lblRes, DetailedMSG, page)
                Case MessageTypesEnum.ERR
                    ShowErrorMessgage(lblRes, ErrorMsg, page, EX)

                Case MessageTypesEnum.CustomErr
                    ShowCustomErrorMessgage(lblRes, DetailedMSG, page)
                Case Else

            End Select

        Catch exx As Exception
        End Try
    End Sub


    Private Shared Sub ShowErrorMessgage(ByRef lblRes As Label, ByVal Message As String, ByVal page As Page, Optional EXX As Exception = Nothing, Optional ByVal DetailedMSG As String = "")
        Try
            ShowMessgage(lblRes, ErrorMsg, "alert alert-danger", page)

        Catch ex As Exception
        End Try
    End Sub
    Private Shared Sub ShowCustomErrorMessgage(ByRef lblRes As Label, ByVal DetailedMSG As String, ByVal page As Page)
        Try
            ShowMessgage(lblRes, DetailedMSG, "alert alert-danger", page)
        Catch ex As Exception
        End Try
    End Sub

    Private Shared Sub ShowCustomMessgage(ByRef lblRes As Label, ByVal message As String, ByVal page As Page)
        Try
            ShowMessgage(lblRes, message, "alert alert-warning", page)
        Catch ex As Exception
        End Try
    End Sub
    Private Shared Sub ShowMessgage(ByRef lblRes As Label, ByVal message As String, ByVal cssClass As String, ByVal page As Page)
        Try
            lblRes.Text = message.ToString
            lblRes.Visible = True
            lblRes.CssClass = cssClass
            ScriptManager.RegisterClientScriptBlock(page, page.[GetType](), "", "$('#" + lblRes.ClientID + "');", True)

        Catch ex As Exception
        End Try
    End Sub
#End Region

#Region "Direct Way"
    Public Shared Sub ShowErrorMessgage(ByRef lblRes As Label, ByVal message As String, ByVal page As Page)
        Try
            ShowMessgage(lblRes, message, "alert alert-danger", page)
        Catch ex As Exception
        End Try
    End Sub

    Public Shared Sub ShowAlertMessgage(ByRef lblRes As Label, ByVal message As String, ByVal page As Page)
        Try
            ShowMessgage(lblRes, message, "alert alert-warning", page)
        Catch ex As Exception
        End Try
    End Sub

    Public Shared Sub ShowSuccessMessgage(ByRef lblRes As Label, ByVal message As String, ByVal page As Page)
        Try
            ShowMessgage(lblRes, message, "alert alert-success", page)
        Catch ex As Exception
        End Try
    End Sub

    Public Shared Sub ShowInfoMessgage(ByRef lblRes As Label, ByVal message As String, ByVal page As Page)
        Try
            ShowMessgage(lblRes, message, "lbl-validate", page)
        Catch ex As Exception
        End Try
    End Sub


#End Region
End Class