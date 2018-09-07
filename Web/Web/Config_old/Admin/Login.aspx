<%@ Page Language="C#" AutoEventWireup="true" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1">
    <title></title>
    <script src="js/yk.js" type="text/javascript"></script>
    <style>
        body, div, span, p, li, ul,table,tr,td {
            margin:0px;
            padding:0px;
        }
        body {
            background-image:url(images/banner.jpg);
        }
        .loginDiv {
            border:1px solid #C1C1C1;
            width:400px;
            height:300px;
            margin:auto;
            background-color:white;
            filter:alpha(opacity=80); /*IE滤镜，透明度50%*/
            -moz-opacity:0.8; /*Firefox私有，透明度50%*/
            opacity:0.8;/*其他，透明度50%*/
        }
        .loginTitle {
            font-weight:bold;
            font-size:22px;
            padding:10px;
        }
        #orgCode,#orgCodeTitle,#userName, #userPwd,#userNameTitle,#userPwdTitle,#verCode,#verCodeTitle {
            width:280px;
            height:25px;
            line-height:25px;
            border:1px solid #C1C1C1;
        }
        #orgCodeTitle,#userNameTitle, #userPwdTitle,#verCodeTitle {
            color:gray;
        }
        #btnSubmit {
            width:100px;
            height:25px;
            line-height:25px;
            text-align:center;
            color:white;
            background-color:#FF8500;
            border:0px;
        }
        table {
            margin:5px 40px;
            height:220px;
        }
        .tooltipDiv
        {
            font-size:12px;
            color:Red;
            padding-left:40px;
            }
    </style>
    <script type="text/javascript">
        function changeUserName() {
            document.getElementById("userName").style.display = "block";
            document.getElementById("userNameTitle").style.display = "none";
            document.getElementById("userName").focus();
        }
        function changeUserPwd() {
            document.getElementById("userPwd").style.display = "block";
            document.getElementById("userPwdTitle").style.display = "none";
            document.getElementById("userPwd").focus();
        }
        function changeVerCode() {
            document.getElementById("verCode").style.display = "block";
            document.getElementById("verCodeTitle").style.display = "none";
            document.getElementById("verCode").focus();
        }
        function changeUserNameCheck() {
            if (document.getElementById("userName").value == "") {
                document.getElementById("userName").style.display = "none";
                document.getElementById("userNameTitle").style.display = "block";
            }
        }
        function changeUserPwdCheck() {
            if (document.getElementById("userPwd").value == "") {
                document.getElementById("userPwd").style.display = "none";
                document.getElementById("userPwdTitle").style.display = "block";
            }
        }
        function changeVerCodeCheck() {
            if (document.getElementById("verCode").value == "") {
                document.getElementById("verCode").style.display = "none";
                document.getElementById("verCodeTitle").style.display = "block";
            }
        }
        function checkLogin() {
            var isOk = true;
            var userName=document.getElementById("userName").value;
            var userPwd=document.getElementById("userPwd").value;
            var verCode=document.getElementById("verCode").value;
            if (userName == "") {
                document.getElementById("userNameTitle").style.border = "1px solid red";
                isOk = false;
            }
            if (userPwd == "") {
                document.getElementById("userPwdTitle").style.border = "1px solid red";
                isOk = false;
            }
            if (verCode == "") {
                document.getElementById("verCodeTitle").style.border = "1px solid red";
                isOk = false;
            }
            if (isOk == true) {
                var returnMsg = yk.post("ajax/login.ashx", "userName=" + userName + "&userPwd=" + userPwd + "&verCode=" + verCode, null);
                if (returnMsg == "1") {
                    window.location = "Default.aspx";
                }
                else {
                    document.getElementById("tooltip").innerHTML = returnMsg;
                }
            }
        }
        window.onkeydown = function ()
        {
            if (event.keyCode == 13)
            {
                document.getElementById("btnSubmit").click();
            }
        }
    </script>
</head>
<body>
    <div class="loginDiv" id="loginDiv">
        <div class="loginTitle">账号登录</div>
        <table>
            <tr>
                <td colspan="2">
                    <input type="text" id="userNameTitle" value=" 请输入用户名" onfocus="changeUserName()" />
                    <input type="text" id="userName" title="用户名"  style="display:none;" onblur="changeUserNameCheck()" />
                </td>                
            </tr>
            <tr>
                <td colspan="2">
                    <input type="text" id="userPwdTitle" value=" 请输入密码" onfocus="changeUserPwd()" />
                    <input type="password" id="userPwd" title="密码" style="display:none;" onblur="changeUserPwdCheck()" />
                </td>
            </tr>
            <tr>
                <td style="width:110px;">
                    <input type="text" id="verCodeTitle" value=" 请输入验证码" style="width:100px;" onfocus="changeVerCode()" />
                    <input type="text" id="verCode" style="width:100px; display:none;" onblur="changeVerCodeCheck()" />                    
                </td>
                <td>
                    <img src="VerifyCode.aspx" width="60px" height="20px" onclick="this.src+='?'" 
                        title="看不清楚，换一张" style="cursor:pointer;"/>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <input type="button" id="btnSubmit" value="登 录" onclick="checkLogin()"/>
                </td>
            </tr>
        </table> 
        <div class="tooltipDiv"><span id="tooltip"></span></div>
        <script language="javascript" type="text/javascript">
            document.getElementById("loginDiv").style.marginTop = ((window.innerHeight - 250) / 2 - 100) + "px";
        </script>
    </div>
</body>
</html>
