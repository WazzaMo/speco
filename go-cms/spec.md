# Golang CMS

This project is an API that is a Lambda Function invoked from API Gateway.
The lambda function uses the input page path to look up an S3 object that holds the
configured content and the HTML template and returns a populated page.
Meaning the API maps the page path to:
1. content JSON data
2. HTML template needed with references to content to fill in the page.

- language: go-lang
- platform: aws lambda
- input: page path
- response: HTML page

## Content JSON

### Example

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

### Content JSON Specification

Content JSON has two mandatory properties:

1. "template" = string that gives the S3 object path to the ghtml template file.
2. "content" = JSON object with properties holding strings, arrays of objects, objects and numbers where the properties are defined by the template.


## Template ghtml files

### Example

```html
<body>
    <title>%$title%</title>
    <div %for block in $blocks%>
        <h2>%block.$sub_title%</h2>
        <p>%block.$body%</p>
    </div>
</body>
```

### Template gthml Specification

Templates mix regular HTML content and Template code.

Template code is enclosed with `%` markers and should evaluate to a string.
Keys to find in Content JSON is prefixed with a `$` and can be a property of a variable.

Template code supports:
1. references to content keys which directly insert the value from Content JSON into the template
2. for loops that handle variable length arrays from Content JSON, in the format of `for <variable> in $<key>`
3. variables holding an instance from an array will refer to a JSON sub object and will have $ properties for each
property in that sub-object.


