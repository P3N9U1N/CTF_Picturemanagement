Picture Management System:  
Web app that showcases the directory traversal attack, meant to be used for CTF
challenges. 
First stage:
Successfully download the file /usr/share/flag.txt that contains the flag.
The flag can be customized in the _flag.txt file.
Second stage:
Download /etc/passwd and /etc/shadow and crack the password (abc123).
John the ripper can be used for this. The flag is the sha1sum of the
password: "flag_6367c48dd193d56ea7b0baad25b19455e529f5ee_".

Building requirements:  
.Net SDK 5.0 and typescript. Docker is optional.

Running:  
Run "dotnet run" from main directory. 
Navigate to http://localhost:5003 with browser.

Docker instructions:  
Run "dotnet publish -c Release" from the main directory.
Now you can create a docker image with
"docker build -t picture-management -f Dockerfile ."
and run with (default internal port is set to 5000):
"docker run --rm -d -p 8080:5000 --name myapp picture-management"


Pictures:  
Feel free to use your own pictures.
Just copy them to the wwwroot/pics directory, they are recognized
automatically. 

Useful Docker commands:  
dotnet publish -c Release  
docker build -t picture-management -f Dockerfile.  
docker run --rm -d -p 8080:5000 --name myapp picture-management  
docker save -o picture-management.tar picture-management  
docker load -i picture-management.tar  


