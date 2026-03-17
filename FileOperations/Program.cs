using System.Text;
using System.IO;

// Create sample sales data files for demonstration
CreateSampleSalesFiles();

// Generate and display sales summary
GenerateSalesSummaryReport();

void CreateSampleSalesFiles()
{
    string salesDirectory = "sales_data";
    
    // Create directory if it doesn't exist
    if (!Directory.Exists(salesDirectory))
    {
        Directory.CreateDirectory(salesDirectory);
    }

    // Create sample sales files
    File.WriteAllText(Path.Combine(salesDirectory, "store1_sales.txt"), "1250000.50");
    File.WriteAllText(Path.Combine(salesDirectory, "store2_sales.txt"), "980000.75");
    File.WriteAllText(Path.Combine(salesDirectory, "store3_sales.txt"), "1150000.25");
}

void GenerateSalesSummaryReport()
{
    string salesDirectory = "sales_data";
    string reportPath = "sales_summary.txt";

    StringBuilder report = new StringBuilder();
    decimal totalSales = 0;
    
    // Read all sales files
    string[] salesFiles = Directory.GetFiles(salesDirectory, "*_sales.txt");
    
    if (salesFiles.Length == 0)
    {
        Console.WriteLine("No sales files found.");
        return;
    }

    report.AppendLine("Sales Summary");
    report.AppendLine("----------------------------");
    report.AppendLine();

    // Calculate total sales
    Dictionary<string, decimal> filesSales = new Dictionary<string, decimal>();
    
    foreach (string file in salesFiles)
    {
        string content = File.ReadAllText(file);
        if (decimal.TryParse(content, out decimal sales))
        {
            totalSales += sales;
            filesSales[Path.GetFileName(file)] = sales;
        }
    }

    // Write total sales
    report.AppendLine($"Total Sales: {totalSales:C}");
    report.AppendLine();
    report.AppendLine("Details:");

    // Write individual file details
    foreach (var kvp in filesSales)
    {
        report.AppendLine($"  {kvp.Key}: {kvp.Value:C}");
    }

    // Write report to file
    File.WriteAllText(reportPath, report.ToString());

    // Display report to console
    Console.WriteLine(report.ToString());
    Console.WriteLine($"Report saved to: {reportPath}");
}
