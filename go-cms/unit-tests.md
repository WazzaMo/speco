# Unit Tests

The lambda function code must have a one or more unit tests

## happy path tests

### test: example content and template generates expected html

setup:
- mock the function that reads the content from S3 to return
```
{
    "template": "/templates/page1.ghtml",
    "content": {
        "title": "using S3 as a CMS database",
        "blocks": [
            {
                "sub_title": "Why?",
                "body": "Mostly because it's very cheap, highly available and spans availability zones."
            }
        ]
    }
}
```
- mock the function that loads the template, so that for path "/templates/page1.ghtml" it returns:
```
<body>
    <title>%$title%</title>
    <div %for block in $blocks%>
        <h2>%block.$sub_title%</h2>
        <p>%block.$body%</p>
    </div>
</body>
```

execute:
- run the lambda entry point function

expect:
The returned string must equal:
```
<body>
    <title>using S3 as a CMS database</title>
    <div>
        <h2>Why?</h2>
        <p>Mostly because it's very cheap, highly available and spans availability zones.</p>
    </div>
</body>
```
