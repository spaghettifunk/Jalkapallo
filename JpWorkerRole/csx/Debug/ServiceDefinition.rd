<?xml version="1.0" encoding="utf-8"?>
<serviceModel xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" name="JpWorkerRole" generation="1" functional="0" release="0" Id="4a2c38e5-ad7e-4e72-8285-1b3417a291c7" dslVersion="1.2.0.0" xmlns="http://schemas.microsoft.com/dsltools/RDSM">
  <groups>
    <group name="JpWorkerRoleGroup" generation="1" functional="0" release="0">
      <settings>
        <aCS name="JobSchedulerWR:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="">
          <maps>
            <mapMoniker name="/JpWorkerRole/JpWorkerRoleGroup/MapJobSchedulerWR:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </maps>
        </aCS>
        <aCS name="JobSchedulerWRInstances" defaultValue="[1,1,1]">
          <maps>
            <mapMoniker name="/JpWorkerRole/JpWorkerRoleGroup/MapJobSchedulerWRInstances" />
          </maps>
        </aCS>
      </settings>
      <maps>
        <map name="MapJobSchedulerWR:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" kind="Identity">
          <setting>
            <aCSMoniker name="/JpWorkerRole/JpWorkerRoleGroup/JobSchedulerWR/Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </setting>
        </map>
        <map name="MapJobSchedulerWRInstances" kind="Identity">
          <setting>
            <sCSPolicyIDMoniker name="/JpWorkerRole/JpWorkerRoleGroup/JobSchedulerWRInstances" />
          </setting>
        </map>
      </maps>
      <components>
        <groupHascomponents>
          <role name="JobSchedulerWR" generation="1" functional="0" release="0" software="C:\Users\berdin.TXTMI00\Documents\Visual Studio 2010\Projects\JP\JPManager\Jalkapallo Solution\JPManager\JpWorkerRole\csx\Debug\roles\JobSchedulerWR" entryPoint="base\x64\WaHostBootstrapper.exe" parameters="base\x64\WaWorkerHost.exe " memIndex="1792" hostingEnvironment="consoleroleadmin" hostingEnvironmentVersion="2">
            <settings>
              <aCS name="Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="" />
              <aCS name="__ModelData" defaultValue="&lt;m role=&quot;JobSchedulerWR&quot; xmlns=&quot;urn:azure:m:v1&quot;&gt;&lt;r name=&quot;JobSchedulerWR&quot; /&gt;&lt;/m&gt;" />
            </settings>
            <resourcereferences>
              <resourceReference name="DiagnosticStore" defaultAmount="[4096,4096,4096]" defaultSticky="true" kind="Directory" />
              <resourceReference name="EventStore" defaultAmount="[1000,1000,1000]" defaultSticky="false" kind="LogStore" />
            </resourcereferences>
          </role>
          <sCSPolicy>
            <sCSPolicyIDMoniker name="/JpWorkerRole/JpWorkerRoleGroup/JobSchedulerWRInstances" />
            <sCSPolicyUpdateDomainMoniker name="/JpWorkerRole/JpWorkerRoleGroup/JobSchedulerWRUpgradeDomains" />
            <sCSPolicyFaultDomainMoniker name="/JpWorkerRole/JpWorkerRoleGroup/JobSchedulerWRFaultDomains" />
          </sCSPolicy>
        </groupHascomponents>
      </components>
      <sCSPolicy>
        <sCSPolicyUpdateDomain name="JobSchedulerWRUpgradeDomains" defaultPolicy="[5,5,5]" />
        <sCSPolicyFaultDomain name="JobSchedulerWRFaultDomains" defaultPolicy="[2,2,2]" />
        <sCSPolicyID name="JobSchedulerWRInstances" defaultPolicy="[1,1,1]" />
      </sCSPolicy>
    </group>
  </groups>
</serviceModel>