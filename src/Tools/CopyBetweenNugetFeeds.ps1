Param(
    [string]$version,
    [string]$server, 
    [string]$internalServer, 
    [string]$apiKey
)


### Configuration

$packages = @(
    [pscustomobject]@{ Package = "DotVVM.Diagnostics.ViewHotReload"; Directory = "DotVVM.Diagnostics.ViewHotReload" },
    [pscustomobject]@{ Package = "DotVVM.Diagnostics.ViewHotReload.AspNetCore"; Directory = "DotVVM.Diagnostics.ViewHotReload.AspNetCore" },
    [pscustomobject]@{ Package = "DotVVM.Diagnostics.ViewHotReload.Owin"; Directory = "DotVVM.Diagnostics.ViewHotReload.Owin" }
)

foreach($package in $packages){

    $packageId = $package.Package
	
    # get package
	& .\tools\nuget.exe install $packageId -OutputDirectory .\tools\packages -version $version -DirectDownload -NoCache -DependencyVersion Ignore -source $internalServer
	$nupkgFile = dir -s ./tools/packages/$packageId.$version.nupkg | Select -First 1
	Write-Host "Downloaded package located on '$nupkgFile'"
		
	if ($nupkgFile) {
		# upload 
		Write-Host "Uploading package..."
		& .\tools\nuget.exe push $nupkgFile -source $server -apiKey $apiKey
		Write-Host "Package uploaded to $server."
	}
	if ( Test-Path -Path ./tools/packages ) {
		Remove-Item -Recurse -Force ./tools/packages
	}

}