function showTime(){
	var today = new Date();
	var h=checkTime(today.getHours());
	var m=checkTime(today.getMinutes());
	var s=checkTime(today.getSeconds());
	
	document.getElementById('time').innerHTML=today.getDate() + "." + (today.getMonth() + 1) + "." + today.getFullYear() + " || " + h + ":" + m + ":" + s;
	t=setTimeout("showTime()",1000);
}

function checkTime(i){
	if (i<10) {i="0"+i;}
	return i;
}