Server 쪽 프로젝트 네임스페이스 규칙과 폴더 구조를 따라주면 깔끔하지 않을까 싶음.
.csproj 파일 안에 <OutputPath>$(SolutionDir)\bin\Client\Debug\</OutputPath> 로 통일하는걸 추천.
바이너리들이 외부의 bin 폴더로 한번에 모일 수 있도록.