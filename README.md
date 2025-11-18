# XIRR Demo Solution

This repository contains a .NET 8 solution for demonstrating and calculating the Extended Internal Rate of Return (XIRR). The solution includes the following components:

## Projects

1. **TestApp**
   - A console application that demonstrates the calculation of XIRR using sample data.

2. **Api**
   - A minimal API that exposes an endpoint for calculating XIRR.

3. **XIRREngine**
   - A shared library containing the XIRR calculation logic.

4. **XIRREngine.UnitTests**
   - XUnit tests to validate the XIRR calculation logic.

## Features

- **XIRR Calculation**: Implements the Newton-Raphson method with a fallback to the bisection method for robust XIRR calculation.
- **Console Demonstration**: Demonstrates the calculation of XIRR for provided sample data.
- **API Endpoint**: Provides an HTTP endpoint for calculating XIRR.
- **Unit Tests**: Validates the correctness of the XIRR calculation logic.

## Usage

### Prerequisites

- .NET 8 SDK

### Build and Run

1. Clone the repository:

   ```bash
   git clone <repository-url>
   cd xirr-copilot
   ```

2. Build the solution:

   ```bash
   dotnet build XIRREngine.sln
   ```

3. Run the TestApp:

   ```bash
   dotnet run --project TestApp
   ```

4. Run the API:

   ```bash
   dotnet run --project Api
   ```

5. Run the unit tests:

   ```bash
   dotnet test
   ```

6. Run the TestApi to interact with the API:

   ```bash
   dotnet run --project TestApi
   ```

## Sample Data

The solution includes sample data for testing the XIRR calculation:

- **Sample 1**:

  ```json
  {
      ((2012, 6, 1), 0.01),
      ((2012, 7, 23), 3042626.18),
      ((2012, 11, 7), -491356.62),
      ((2012, 11, 30), 631579.92),
      ((2012, 12, 1), 19769.5),
      ((2013, 1, 16), 1551771.47),
      ((2013, 2, 8), -304595),
      ((2013, 3, 26), 3880609.64),
      ((2013, 3, 31), -4331949.61)
  }
  ```

- **Sample 2**:

  ```json
  {
      ((2001, 5, 1), 10000),
      ((2002, 3, 1), 2000),
      ((2002, 5, 1), -5500),
      ((2002, 9, 1), 3000),
      ((2003, 2, 1), 3500),
      ((2003, 5, 1), -15000)
  }
  ```

## License

This project is licensed under the MIT License.
