<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Match.ascx.cs" Inherits="Admin_Controls_Match" %>
<style type="text/css">
#matchDiv<%=newGuid%>
{
position:absolute;
display:none;
font-size:13px;
background-color:white;
border:1px solid gray;
padding:3px;
}
#matchDiv<%=newGuid%> ul
{
padding:0px;
margin:0px;
width:100%;
height:300px;
overflow:scroll;
}
#matchDiv<%=newGuid%> ul li
{
padding:0px;
margin:0px;
list-style-type:none;
text-align:left;
cursor:pointer;
line-height:20px;
}
</style>
<script type="text/javascript">
//设置信息
function setText<%=newGuid%>(val,name)
{
   document.getElementById("<%=TbText.ClientID%>").value=name;
   document.getElementById("<%=HiddenKey.ClientID%>").value=val;
   //设置后隐藏匹配框
   hiddenMatchDiv<%=newGuid%>();
   //还原值
   nextOrPrevious=-1;
}
//记录上下键的序号值
var nextOrPrevious=-1;
function matchDiv<%=newGuid%>()
{
	 //当前按键的值
	var num=event.keyCode;
	//当按上下键、Enter键的时候不需要重新获取匹配数据    
	if(num!=38&&num!=40&&num!=13)
	{
	  var val=document.getElementById("<%=TbText.ClientID%>").value;
	  var liStr="<ul>";
	  $.ajax({
			type:"get",
			cache:false,
			async:false,
			url:"<%=YK.Common.CommonClass.AppPath%>key.ashx?tableName=<%=TableName%>&fieldName=<%=FieldName%>&key="+val,
			contentType:"application/json;charset=utf-8",
			dataType:"json",
			success:function(msg)
			{
			   var arr=eval(msg);
			   if(arr.length>0)
				 {
				document.getElementById("matchDiv<%=newGuid%>").style.display="block";
				 }
				 else
				 {
				document.getElementById("matchDiv<%=newGuid%>").style.display="none";		
				 }
				 
				 for(var i=0;i<arr.length;i++)
				 {
				 liStr+="<li onclick=\"setText<%=newGuid%>("+arr[i].value+",this.innerText)\" onmouseover=\"this.style.backgroundColor='#E9EEFC'\" onmouseout=\"this.style.backgroundColor='white'\">"+arr[i].name+"</li>";
				 }
			},
			error:function(err)		
			{
			   alert("操作错误");
			}
		});
		liStr+="</ul>";
		var ulList=document.getElementById("matchDiv<%=newGuid%>");
		ulList.innerHTML=liStr;
	}
	var arr=document.getElementById("matchDiv<%=newGuid%>").getElementsByTagName("li");
	if(num!=13)
	{
		//循环赋值白色，与背景色一致
		for(var i=0;i<arr.length;i++)
		{
			arr[i].style.backgroundColor="white";
		}	
		switch(num)
		{
			case 38:
				if(nextOrPrevious>0)
				{
					nextOrPrevious=nextOrPrevious-1;
					arr[nextOrPrevious].style.backgroundColor="#E9EEFC";
				}
				break;
			case 40:
				if(nextOrPrevious<arr.length-1)
				{
					nextOrPrevious=nextOrPrevious+1;
					arr[nextOrPrevious].style.backgroundColor="#E9EEFC";
				}
				break;	
		}
	}
	else
	{
		arr[nextOrPrevious].click();
	}
}
//显示匹配信息
function showMatchDiv<%=newGuid%>()
{
	if(document.getElementById("matchDiv<%=newGuid%>").style.display=="none")
	{
		document.getElementById("matchDiv<%=newGuid%>").style.display="block";	
	}
}
//隐藏匹配信息
function hiddenMatchDiv<%=newGuid%>()
{
	document.getElementById("matchDiv<%=newGuid%>").style.display="none";
}
//禁用Enter键自动表单提交
document.onkeydown=function(event)
{
	var target,code,tag;
	if(!event)
	{
		event=window.event;
		target=event.srcElement;
		code=event.keyCode;
	}	
	else
	{
		target=event.target;//针对遵循W3C的浏览器
		code=event.keyCode;
	}
	if(code==13)
	{
		tag=target.tagName;
		if(tag=="intput"||tag=="textarea")
		{
			return true;
		}
		else
		{
			return false;	
		}
	}
}
</script>
<div>
    <asp:TextBox runat="server" ID="TbText"></asp:TextBox>
    <div id="matchDiv<%=newGuid%>" onmouseover="showMatchDiv<%=newGuid%>()">
    </div>
    <asp:HiddenField runat="server" ID="HiddenKey" />
</div>
