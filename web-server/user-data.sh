#!/bin/bash
yum update -y
yum install -y httpd
systemctl start httpd
systemctl enable httpd
yum install -y git
echo "<h1>Hello World!!!</h1>" > /var/www/html/index.html