var express = require('express');
var app = express();
var cheerio = require('cheerio');
var superagent = require('superagent');

//list?url=http://www.qupu123.com/qiyue/dianziqin
app.get('/list', function (req, res) {
    console.log(req.query.url);
    superagent.get(req.query.url).end(function(err,sres){
        if(err){
            return next(err);
        }

       var result=new Array();

       var $=cheerio.load(sres.text);
       $('table tbody tr').each(function(i,item){
            
            if($(item).children().eq(0).text()!==''){
                var tr={};
                tr['Title']=$(item).children().eq(1).text();
                tr['Type']=$(item).children().eq(2).text();
                tr['Songwriter']=$(item).children().eq(3).text();
                tr['Singer']=$(item).children().eq(4).text();
                tr['Uploader']=$(item).children().eq(5).text();
                tr['UploadDate']=$(item).children().eq(6).text();
                tr['ResourceLink']=$(item).children().eq(1).children().eq(0).attr('href');
            
                result.push(JSON.stringify(tr));
            }  
        });
       
        res.send(JSON.stringify(result));
    });
});

//get?url=http://www.qupu123.com/qiyue/dianziqin/p326589.html
app.get('/get', function (req, res) {
    console.log(req.query.url);
    superagent.get(req.query.url).end(function(err,sres){
        if(err){
            return next(err);
        }

        var result=new Array();

        var $=cheerio.load(sres.text);
        $('.imageList').each(function(i,item){
            $(item).children('a').each(function(j,citem){
                result.push(JSON.stringify($(citem).attr('href')));
            });
        });
       
        res.send(JSON.stringify(result));
    });
});
 
var server = app.listen(10003, function () {
 
    var host = server.address().address;
    var port = server.address().port;
 
    console.log("应用实例，访问地址为 http://%s:%s", host, port);
});