# RealEstate API

A comprehensive .NET 8.0 Web API solution for managing real estate companies and apartments with enhanced webhook support for external integrations.

## Features

- **Company Management**: Retrieve all companies and their details
- **Apartment Management**: Get apartments by company, including expiring contracts
- **Enhanced Webhook Integration**: Real-time updates with comprehensive validation and error reporting
- **Modern UI**: Blazor Server frontend with Bootstrap styling
- **Database**: SQL Server with Entity Framework Core
- **Comprehensive Logging**: Detailed audit trails for all operations

## API Endpoints

### Companies
- `GET /api/RealEstate/companies` - Get all companies
- `GET /api/RealEstate/companies/{companyId}/apartments` - Get apartments for a specific company
- `GET /api/RealEstate/companies/{companyId}/contracts/expiring?months=3` - Get apartments with expiring contracts

### Webhooks
- `POST /api/Webhook/apartment-attribute` - Update apartment attributes from external systems
- `GET /api/Webhook/health` - Health check for webhook service

## Webhook Integration

### Overview
The webhook endpoint allows external systems to update apartment attributes in real-time with enhanced validation, comprehensive error reporting, and detailed logging. This is useful for:
- Property management systems updating occupancy status
- Rental platforms syncing rent changes
- Maintenance systems updating apartment details
- Integration with IoT devices (smart locks, sensors)
- Real-time synchronization between multiple systems

### New Webhook Architecture

The webhook functionality has been refactored into a dedicated architecture:

- **`WebhookController`**: Dedicated controller for webhook operations
- **`ApartmentService`**: Service layer with comprehensive validation and error handling
- **`WebhookUpdateResult`**: Detailed result model with validation errors and context
- **Enhanced Validation**: Comprehensive input validation with specific error messages
- **Verbose Error Reporting**: Detailed feedback for debugging and integration

### Webhook Payload

```json
{
  "apartmentId": "123e4567-e89b-12d3-a456-426614174000",
  "isOccupied": true,
  "rentPerMonth": 15000.00,
  "leaseEndDate": "2024-12-31T23:59:59Z",
  "floor": 3,
  "rooms": 2,
  "address": "Storgatan 15, Stockholm",
  "sourceId": "property-mgmt-system-001",
  "processedAt": "2024-01-15T10:30:00Z"
}
```

### Supported Attributes
- `apartmentId` (required): Unique identifier of the apartment
- `isOccupied`: Boolean indicating if apartment is currently occupied
- `rentPerMonth`: Monthly rent amount (0 - 1,000,000 SEK)
- `leaseEndDate`: When the current lease expires (within reasonable date range)
- `floor`: Floor number of the apartment (0 - 200)
- `rooms`: Number of rooms (1 - 20)
- `address`: Physical address (5 - 500 characters)
- `sourceId`: Identifier for the external system (for tracking, max 100 chars)
- `processedAt`: Timestamp when the update was processed by the source

### Enhanced Response Format

**Success Response (200):**
```json
{
  "success": true,
  "message": "Apartment attributes updated successfully",
  "apartmentId": "123e4567-e89b-12d3-a456-426614174000",
  "processedAt": "2024-01-15T10:30:15Z",
  "sourceId": "property-mgmt-system-001",
  "httpStatusCode": 200,
  "updatedAttributes": ["IsOccupied", "RentPerMonth"],
  "skippedAttributes": []
}
```

**Validation Error Response (400):**
```json
{
  "success": false,
  "message": "Validation errors occurred while processing webhook",
  "apartmentId": "123e4567-e89b-12d3-a456-426614174000",
  "processedAt": "2024-01-15T10:30:15Z",
  "sourceId": "property-mgmt-system-001",
  "httpStatusCode": 400,
  "errorCode": "VALIDATION_FAILED",
  "validationErrors": [
    {
      "field": "rentPerMonth",
      "message": "Rent per month cannot be negative",
      "invalidValue": -1000,
      "validationRule": "MinValue",
      "context": "Rent must be a positive number"
    }
  ]
}
```

**Not Found Response (404):**
```json
{
  "success": false,
  "message": "Apartment 123e4567-e89b-12d3-a456-426614174000 not found",
  "apartmentId": "123e4567-e89b-12d3-a456-426614174000",
  "processedAt": "2024-01-15T10:30:15Z",
  "sourceId": "property-mgmt-system-001",
  "httpStatusCode": 404,
  "errorCode": "APARTMENT_NOT_FOUND"
}
```

## External Supplier Examples

### Example 1: Property Management System (C#)
```csharp
public async Task UpdateApartmentOccupancyAsync(Guid apartmentId, bool isOccupied)
{
    var payload = new
    {
        apartmentId = apartmentId,
        isOccupied = isOccupied,
        sourceId = "prop-mgmt-v2.1",
        processedAt = DateTime.UtcNow
    };

    using var client = new HttpClient();
    var response = await client.PostAsJsonAsync(
        "https://your-api.com/api/Webhook/apartment-attribute", 
        payload);
    
    if (response.IsSuccessStatusCode)
    {
        var result = await response.Content.ReadFromJsonAsync<WebhookUpdateResult>();
        Console.WriteLine($"Updated apartment {apartmentId}: {result.Message}");
        Console.WriteLine($"Updated attributes: {string.Join(", ", result.UpdatedAttributes)}");
    }
    else
    {
        var errorResult = await response.Content.ReadFromJsonAsync<WebhookUpdateResult>();
        Console.WriteLine($"Update failed: {errorResult.Message}");
        
        if (errorResult.ValidationErrors?.Any() == true)
        {
            foreach (var error in errorResult.ValidationErrors)
            {
                Console.WriteLine($"Field '{error.Field}': {error.Message}");
            }
        }
    }
}
```

### Example 2: Python Integration with Error Handling
```python
import requests
import json
from datetime import datetime

def update_apartment_rent(apartment_id, new_rent):
    payload = {
        "apartmentId": str(apartment_id),
        "rentPerMonth": float(new_rent),
        "sourceId": "python-rental-system",
        "processedAt": datetime.utcnow().isoformat() + "Z"
    }
    
    try:
        response = requests.post(
            "https://your-api.com/api/Webhook/apartment-attribute",
            json=payload,
            headers={"Content-Type": "application/json"}
        )
        
        result = response.json()
        
        if response.status_code == 200:
            print(f"Success: {result['message']}")
            print(f"Updated attributes: {', '.join(result['updatedAttributes'])}")
        else:
            print(f"Error: {response.status_code} - {result['message']}")
            print(f"Error Code: {result['errorCode']}")
            
            if result.get('validationErrors'):
                print("Validation Errors:")
                for error in result['validationErrors']:
                    print(f"  {error['field']}: {error['message']}")
                    print(f"    Invalid value: {error['invalidValue']}")
                    print(f"    Rule: {error['validationRule']}")
                    
    except requests.exceptions.RequestException as e:
        print(f"Network error: {e}")
```

### Example 3: JavaScript/Node.js with Enhanced Error Handling
```javascript
async function updateApartmentLease(apartmentId, newLeaseEndDate) {
    const payload = {
        apartmentId: apartmentId,
        leaseEndDate: newLeaseEndDate.toISOString(),
        sourceId: 'js-lease-manager',
        processedAt: new Date().toISOString()
    };

    try {
        const response = await fetch('/api/Webhook/apartment-attribute', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(payload)
        });

        const result = await response.json();
        
        if (result.success) {
            console.log(`Lease updated: ${result.message}`);
            console.log(`Updated attributes: ${result.updatedAttributes.join(', ')}`);
            console.log(`Processed at: ${new Date(result.processedAt).toLocaleString()}`);
        } else {
            console.error(`Update failed: ${result.message}`);
            console.error(`Error code: ${result.errorCode}`);
            console.error(`HTTP status: ${result.httpStatusCode}`);
            
            if (result.validationErrors) {
                console.error('Validation errors:');
                result.validationErrors.forEach(error => {
                    console.error(`  ${error.field}: ${error.message}`);
                    console.error(`    Invalid value: ${error.invalidValue}`);
                    console.error(`    Rule: ${error.validationRule}`);
                    console.error(`    Context: ${error.context}`);
                });
            }
        }
    } catch (error) {
        console.error('Webhook error:', error);
    }
}
```

## Azure Implementation

### Option 1: Azure Functions (Serverless)
```csharp
[FunctionName("ProcessRealEstateWebhook")]
public static async Task<IActionResult> Run(
    [HttpTrigger(AuthorizationLevel.Function, "post", Route = "webhook")] HttpRequest req,
    ILogger log)
{
    try
    {
        string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
        var webhookData = JsonSerializer.Deserialize<ApartmentAttributeUpdateDto>(requestBody);
        
        // Process the webhook data with enhanced validation
        var result = await apartmentService.UpdateApartmentAttributesAsync(webhookData);
        
        // Return appropriate status code based on result
        return new ObjectResult(result) { StatusCode = result.HttpStatusCode };
    }
    catch (Exception ex)
    {
        log.LogError(ex, "Error processing webhook");
        var errorResult = WebhookUpdateResult.CreateSystemError(
            Guid.Empty, null, ex.Message, "AZURE_FUNCTION_ERROR");
        return new ObjectResult(errorResult) { StatusCode = errorResult.HttpStatusCode };
    }
}
```

### Option 2: Azure Logic Apps with Error Handling
```json
{
  "definition": {
    "triggers": {
      "manual": {
        "type": "Request",
        "kind": "Http",
        "inputs": {
          "schema": {
            "type": "object",
            "properties": {
              "apartmentId": { "type": "string" },
              "isOccupied": { "type": "boolean" }
            }
          }
        }
      }
    },
    "actions": {
      "UpdateDatabase": {
        "type": "Http",
        "inputs": {
          "method": "POST",
          "uri": "@{variables('apiUrl')}/api/Webhook/apartment-attribute",
          "body": "@triggerBody()"
        },
        "runAfter": {},
        "on": {
          "success": {
            "actions": {
              "LogSuccess": {
                "type": "Compose",
                "inputs": "Webhook processed successfully"
              }
            }
          },
          "failure": {
            "actions": {
              "LogError": {
                "type": "Compose",
                "inputs": "@{body('UpdateDatabase')}"
              }
            }
          }
        }
      }
    }
  }
}
```

### Option 3: Azure API Management with Enhanced Monitoring
- **Rate Limiting**: Control webhook call frequency
- **Authentication**: Add API keys or OAuth2
- **Enhanced Monitoring**: Track webhook usage, performance, and error rates
- **Response Transformation**: Modify payloads before reaching your API
- **Error Handling**: Custom error responses and retry logic

## Security Considerations

### Authentication Options
1. **API Keys**: Simple but less secure
2. **OAuth2/JWT**: More secure, suitable for production
3. **Azure AD**: Enterprise-grade security
4. **IP Whitelisting**: Restrict to known sources
5. **Webhook Signatures**: Verify webhook authenticity

### Rate Limiting
- Implement throttling to prevent abuse
- Use Azure API Management or custom middleware
- Monitor and alert on unusual patterns
- Per-source rate limiting for better control

### Data Validation
- Comprehensive validation of all incoming webhook data
- Sanitize inputs to prevent injection attacks
- Log all webhook activities for audit trails
- Validate data types and ranges

## Monitoring and Logging

### Application Insights Integration
```csharp
// Add custom telemetry for webhooks
telemetry.TrackEvent("WebhookReceived", new Dictionary<string, string>
{
    ["SourceId"] = payload.SourceId ?? "Unknown",
    ["ApartmentId"] = payload.ApartmentId.ToString(),
    ["AttributesUpdated"] = GetUpdatedAttributes(payload),
    ["ValidationErrors"] = validationErrors?.Count.ToString() ?? "0"
});

// Track webhook performance
telemetry.TrackDependency("Webhook", "UpdateApartmentAttributes", 
    DateTimeOffset.UtcNow, TimeSpan.FromMilliseconds(sw.ElapsedMilliseconds), true);
```

### Azure Monitor Alerts
- Set up alerts for webhook failures
- Monitor response times and throughput
- Track webhook volume by source
- Alert on validation error patterns

## Getting Started

1. **Clone the repository**
2. **Update connection string** in `appsettings.Development.json`
3. **Run migrations**: `dotnet ef database update`
4. **Start the application**: `dotnet run`
5. **Test webhooks** using the enhanced test client

## Testing Webhooks

### Enhanced Test Client
- **Interactive HTML Interface**: Visual webhook testing
- **Real-time Validation**: See validation errors as you type
- **Comprehensive Error Display**: Detailed error information with context
- **Success Feedback**: Clear indication of what was updated

### Testing Tools
- **Postman**: For manual testing and collection building
- **curl**: Command-line testing and automation
- **Azure Logic Apps**: For automated testing workflows
- **Webhook.site**: For debugging webhook calls

## Migration from Old Webhook Endpoint

The webhook functionality has been moved from `/api/RealEstate/webhooks/apartment-attribute` to `/api/Webhook/apartment-attribute`. 

**Key Changes:**
- Enhanced validation with detailed error messages
- Comprehensive error reporting with field-level details
- Better logging and audit trails
- Improved response structure with HTTP status codes
- Health check endpoint for monitoring

**Update your integrations to use the new endpoint for enhanced functionality.**

## Support

For questions about the webhook integration or Azure implementation, please refer to the API documentation or contact the development team.

