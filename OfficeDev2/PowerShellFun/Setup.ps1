[xml]$xml = Get-Content  .\Setup.xml

Write-Host "What Environment do you want to use?"
$userselectedenvironment = Read-Host

$environment = $xml.setup.environments.ChildNodes | where {$_.name -eq $userselectedenvironment } 

Connect-PnPOnline -Url $environment.url -Credentials $environment.creds

Set-PnPTraceLog -On -level Information -Debug 

foreach ($script in $xml.setup.scripts.ChildNodes)
{
   Write-Host $script.location
   Write-Host $script.type

   if ($script.type -eq "pnpPS")
   {
       try {
        . $script.location
       }
       catch {
            Write-Host  $_.Exception.Message -ForegroundColor Red
            Write-Host "Exception Happened Do you want to continue? (y / n)" -ForegroundColor Yellow
            $continue = Read-Host
            if ($continue -eq "n")
            {
                exit
            }

       }
      
   }

   if ($script.type -eq "pnpXML")
   {
    try {
        Write-Host ("applying pnp Provisioning emplate " + $script.location) -ForegroundColor Green
        Apply-PnPProvisioningTemplate -Path $script.location
        Write-Host ("Done applying pnp Provisioning emplate " + $script.location) -ForegroundColor Green

       }
       catch {
            Write-Host  $_.Exception.Message -ForegroundColor Red
            Write-Host "Exception Happened Do you want to continue? (y / n)" -ForegroundColor Yellow
            $continue = Read-Host
            if ($continue -eq "n")
            {
                exit
            }

       }
   }



}


