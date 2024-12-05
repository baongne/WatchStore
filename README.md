<h1>NHÓM 25: Xây dựng website bán đồng hồ</h1>
## Overview: Dự án này là một trang web quản lý việc kinh doanh các sản phẩm đồng hồ cho một cửa hàng. Trang web được phát triển để cung cấp các chức năng cơ bản cho việc quản lý và bán hàng trực tuyến, bao gồm:

- Đăng nhập và quản lý tài khoản người dùng.
- Hiển thị danh sách các sản phẩm đồng hồ có sẵn để mua.
- Thực hiện các thao tác mua bán sản phẩm.
- Quản lý đơn hàng và theo dõi tình trạng giao hàng.
- Quản lý sản phẩm, bao gồm thêm, sửa, xóa sản phẩm.
- Thống kê dữ liệu về doanh số bán hàng và tổng hợp các thông tin quản lý.

<h3>Languages and Tools:</h3>
  <a href="https://getbootstrap.com" target="_blank" rel="noreferrer">
    <img
      src="https://raw.githubusercontent.com/devicons/devicon/master/icons/bootstrap/bootstrap-plain-wordmark.svg"
      alt="bootstrap"
      width="40"
      height="40"
    />
  </a>
  <a href="https://www.w3schools.com/cs/" target="_blank" rel="noreferrer">
    <img
      src="https://raw.githubusercontent.com/devicons/devicon/master/icons/csharp/csharp-original.svg"
      alt="csharp"
      width="40"
      height="40"
    />
  </a>
  <a href="https://www.w3schools.com/css/" target="_blank" rel="noreferrer">
    <img
      src="https://raw.githubusercontent.com/devicons/devicon/master/icons/css3/css3-original-wordmark.svg"
      alt="css3"
      width="40"
      height="40"
    />
  </a>
  <a href="https://dotnet.microsoft.com/" target="_blank" rel="noreferrer">
    <img
      src="https://raw.githubusercontent.com/devicons/devicon/master/icons/dot-net/dot-net-original-wordmark.svg"
      alt="dotnet"
      width="40"
      height="40"
    />
  </a>
  <a href="https://www.w3.org/html/" target="_blank" rel="noreferrer">
    <img
      src="https://raw.githubusercontent.com/devicons/devicon/master/icons/html5/html5-original-wordmark.svg"
      alt="html5"
      width="40"
      height="40"
    />
  </a>
  <a
    href="https://www.microsoft.com/en-us/sql-server"
    target="_blank"
    rel="noreferrer"
  >
    <img
      src="https://www.svgrepo.com/show/303229/microsoft-sql-server-logo.svg"
      alt="mssql"
      width="40"
      height="40"
    />
  </a>
  <a href="https://angular.dev/installation" target="_blank" rel="noreferrer">
    <img
      src="https://raw.githubusercontent.com/devicons/devicon/master/icons/angular/angular-original-wordmark.svg"
      alt="angular"
      width="40"
      height="40"
    />
</a>

  <a href="https://nodejs.org" target="_blank" rel="noreferrer">
    <img
      src="https://raw.githubusercontent.com/devicons/devicon/master/icons/nodejs/nodejs-original-wordmark.svg"
      alt="nodejs"
      width="40"
      height="40"
    />
  </a>
  <a href="https://www.typescriptlang.org/" target="_blank" rel="noreferrer">
    <img
      src="https://raw.githubusercontent.com/devicons/devicon/master/icons/typescript/typescript-original.svg"
      alt="typescript"
      width="40"
      height="40"
    />
  </a>
</p>

## Built with
![.Net](https://img.shields.io/badge/.NET-5C2D91?style=for-the-badge&logo=.net&logoColor=white)
![Angular](https://img.shields.io/badge/angular-%23E23237.svg?style=for-the-badge&logo=angular&logoColor=white)

![MicrosoftSQLServer](https://img.shields.io/badge/Microsoft%20SQL%20Sever-CC2927?style=for-the-badge&logo=microsoft%20sql%20server&logoColor=white)
![Bootstrap](https://img.shields.io/badge/Bootstrap-563D7C?style=for-the-badge&logo=bootstrap&logoColor=white)

![HTML](https://img.shields.io/badge/HTML-E34F26?style=for-the-badge&logo=html5&logoColor=white)
![CSS](https://img.shields.io/badge/CSS-1572B6?style=for-the-badge&logo=css3&logoColor=white)
![ApexCharts](https://img.shields.io/badge/ApexCharts-FF0000?style=for-the-badge&logo=apexcharts&logoColor=white)
![TypeScript](https://img.shields.io/badge/TypeScript-3178C6?style=for-the-badge&logo=typescript&logoColor=white)

## Run project
### Requierment
- Dotnet version 8 or late
- Angular version 18.2.10 or later
1. Clone this project
```bash
# Include Backend, Frontend
git clone https://github.com/baongne/WatchStore


```
2. Run server 

"ConnectionStrings": {
    "Connection": "Data Source=[YOUR_SERVER_NAME];Initial Catalog=MutiFashion;Integrated Security=True;Encrypt=True;Trust Server Certificate=True"
  }
```
If your use user-password server
```json
    "ConnectionStrings":  {
"Connection":  "Data Source=[YOUR_SERVER_NAME];Initial Catalog=MutiFashion;User Id=sa;Password=[YOUR_PASSWORD];Encrypt=True;Trust Server Certificate=True"

},
```
- Database 
Lệnh chạy add migration trong source EasyBuy-Backend:

® _ Add-Migration "user migration" -StartupProject EasyBuy-Backend -Project
EasyBuy-Backend (để tạo migration)

© . Update-Database -StartupProject EasyBuy-Backend -Project EasyBuy-Backend (để
lưu migration vào csdl)

e  Remove-Migration (lệnh rollback migration)

Lệnh xóa csdi trong SQL Server: Update-Database 0|
```

3.Front-end: Run Client,Admin

Bước 1: vô link trên để tải node js trước: https://nodejs.org/en/download/prebuilt-installer
Bước 2:sau đó vô terminal cài cái này: npm install -g @angular/cli
Bước 3: tiếp theo cd vô project bật terminal cài cái này: npm install
bước 4 vô terminal chạy quyền admin : Set-ExecutionPolicy RemoteSigned -Scope CurrentUser
bước 5: cài boostrap 5 ở terminal : npm install bootstrap@5
chạy project, vô terminal: ng serve
```

Tác giả:

|Member             | Member         | Member              | Member            |Member             |
|-------------------|----------------|---------------------|-------------------|-------------------|
|Nguyễn Hữu Quốc Bảo| Võ Công Anh    | Nguyễn Văn Minh Phúc|Bùi Thành Tài      |Võ Lê Kim Tiễn     |
|-------------------|----------------|---------------------|-------------------|-------------------|
|20%                | 20%           | 20%                 | 20%               |20%                |




#### Contact email:
- [anhvca1234@gmail.com](mailto:anhvca1234@gmail.com)
- [huuquocbao0903@gmail.com](mailto:huuquocbao0903@gmail.com)



