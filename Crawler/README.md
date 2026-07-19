# Crawler

A fast web crawler based on the [Abot2](https://github.com/sjdirect/abot) library.

## Features

- Fast crawling
- Stays within the starting domain
- Automatically creates the required database table on first launch
- Stores discovered URLs in a PostgreSQL database

---

# Installation

1. Download the latest **portable** release from the **Releases** page.
2. Extract the archive.
3. Install the **.NET 8 Runtime** if it is not already installed.
4. Configure the database connection (see below).
5. Run the crawler.

---

# Configuration

Create an environment variable named:

```text
findom_psql_dotnet
```

with your PostgreSQL connection string.

Example:

```text
Host=localhost;Port=5432;Database=findom;Username=postgres;Password=your_password
```

The crawler will automatically create the required database table if it does not already exist.

---

# Running

The release is platform-independent and can be run on Windows, Linux, or macOS.

```bash
dotnet Crawler.dll
```

---

# Command-line Arguments

| Argument  | Short | Description                      |
| --------- | :---: | -------------------------------- |
| `--url`   | `-u`  | Starting URL                     |
| `--count` | `-c`  | Maximum number of pages to crawl |

## Example

```bash
dotnet Crawler.dll \
    --url https://example.com \
    --count 100
```

or

```bash
dotnet Crawler.dll -u https://example.com -c 100
```

---

# Building from Source

Clone the repository:

```bash
git clone <repository-url>
cd Crawler
```

Restore dependencies:

```bash
dotnet restore
```

Build:

```bash
dotnet build -c Release
```

Publish a portable release:

```bash
dotnet publish -c Release
```

The published files will be located in:

```text
bin/Release/net8.0/publish/
```
