#requires -Modules UnitySetup

$unityVersions = "2021.3.9f1"
#$unityVersions = "2020.3.18f1","2020.3.15f2"
#2021.3.3f1 has issues with the installer, for instance it doesn't have a version string in the install so all automatic commands fail. you can run this manually from a path, but that is always ugly. also the installer fails with a cryptic message

foreach($unityVersion in $unityVersions)
{
    #install relevant version if not already present
    $alreadyHas = Get-UnitySetupInstance | Select-UnitySetupInstance -Version $unityVersion
    if(!$alreadyHas){
        Find-UnitySetupInstaller -Version $unityVersion -Components 'Android' | Install-UnitySetupInstance
    }

    #Start-UnityEditor -BuildTarget Android -ExecuteMethod TestBuilder.Build -BatchMode -Quit -LogFile ".\build$(get-date -f yyyy-MM-dd-hh-ss).log" -Wait -Version $unityVersion
}
foreach($unityVersion in $unityVersions)
{
    #build with each version
    #run only version sthat installed successfully
    $alreadyHas = Get-UnitySetupInstance | Select-UnitySetupInstance -Version $unityVersion
    if(!$alreadyHas){
        continue; #if installation of that version of unity failed, skip it
    }
    Start-UnityEditor -BuildTarget Android -ExecuteMethod TestBuilder.Build -BatchMode -Quit -LogFile ".\build$(get-date -f yyyy-MM-dd-hh-ss).log" -Wait -Version $unityVersion
}