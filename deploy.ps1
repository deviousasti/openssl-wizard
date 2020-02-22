msbuild
choco pack

$package = Get-ChildItem "*.nupkg" | Select-Object -First 1
if($package -ne $null)
{
    choco push $package.Name --source https://push.chocolatey.org/
}
