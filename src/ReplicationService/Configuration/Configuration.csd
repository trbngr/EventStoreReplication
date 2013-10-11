<?xml version="1.0" encoding="utf-8"?>
<configurationSectionModel xmlns:dm0="http://schemas.microsoft.com/VisualStudio/2008/DslTools/Core" dslVersion="1.0.0.0" Id="70b7fc65-c431-4105-9f20-8dd11437ebb8" namespace="ReplicationService.Configuration" xmlSchemaNamespace="urn:ReplicationService.Configuration" xmlns="http://schemas.microsoft.com/dsltools/ConfigurationSectionDesigner">
  <typeDefinitions>
    <externalType name="String" namespace="System" />
    <externalType name="Boolean" namespace="System" />
    <externalType name="Int32" namespace="System" />
    <externalType name="Int64" namespace="System" />
    <externalType name="Single" namespace="System" />
    <externalType name="Double" namespace="System" />
    <externalType name="DateTime" namespace="System" />
    <externalType name="TimeSpan" namespace="System" />
  </typeDefinitions>
  <configurationElements>
    <configurationSection name="ReplicationServiceConfiguration" codeGenOptions="Singleton, XmlnsProperty" xmlSectionName="replicationService">
      <attributeProperties>
        <attributeProperty name="SliceSize" isRequired="true" isKey="false" isDefaultCollection="false" xmlName="sliceSize" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/70b7fc65-c431-4105-9f20-8dd11437ebb8/Int32" />
          </type>
        </attributeProperty>
        <attributeProperty name="Interval" isRequired="false" isKey="false" isDefaultCollection="false" xmlName="interval" isReadOnly="false" defaultValue="&quot;00:15:00&quot;">
          <type>
            <externalTypeMoniker name="/70b7fc65-c431-4105-9f20-8dd11437ebb8/TimeSpan" />
          </type>
        </attributeProperty>
      </attributeProperties>
      <elementProperties>
        <elementProperty name="SourceEventStore" isRequired="true" isKey="false" isDefaultCollection="false" xmlName="sourceEventStore" isReadOnly="false">
          <type>
            <configurationElementMoniker name="/70b7fc65-c431-4105-9f20-8dd11437ebb8/EventStoreElement" />
          </type>
        </elementProperty>
        <elementProperty name="TargetEventStore" isRequired="true" isKey="false" isDefaultCollection="false" xmlName="targetEventStore" isReadOnly="false">
          <type>
            <configurationElementMoniker name="/70b7fc65-c431-4105-9f20-8dd11437ebb8/EventStoreElement" />
          </type>
        </elementProperty>
      </elementProperties>
    </configurationSection>
    <configurationElement name="EventStoreElement">
      <attributeProperties>
        <attributeProperty name="Host" isRequired="true" isKey="false" isDefaultCollection="false" xmlName="host" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/70b7fc65-c431-4105-9f20-8dd11437ebb8/String" />
          </type>
        </attributeProperty>
        <attributeProperty name="TcpPort" isRequired="true" isKey="false" isDefaultCollection="false" xmlName="tcpPort" isReadOnly="false">
          <type>
            <externalTypeMoniker name="/70b7fc65-c431-4105-9f20-8dd11437ebb8/Int32" />
          </type>
        </attributeProperty>
      </attributeProperties>
    </configurationElement>
  </configurationElements>
  <propertyValidators>
    <validators />
  </propertyValidators>
</configurationSectionModel>