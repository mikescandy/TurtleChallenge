using FluentValidation.Results;
using Newtonsoft.Json;
using TurtleChallenge.App;
using TurtleChallenge.App.Models;
using TurtleChallenge.App.Validators;
using Xunit;

namespace TurtleChallenge.Test
{
    public class SettingsValidatorTest
    {
        [Fact]
        public void ValidSettingsTest()
        {
            string json = @"{
                              'board': {
                                'width': 6,
                                'height': 6,
                                'mines':[
                                    { 'x':0, 'y':1},
                                    { 'x':3, 'y':2},
                                     { 'x':1, 'y':4}
                                    ]
                              },
                                'startTile':{'x':0,'y':0},
                                'exitTile':{'x':5, 'y':4},
                                'direction':'North'
                            }";

            var result = DeserializeAndValidate(json);

            Assert.True(result.IsValid);
        }

        [Fact]
        public void InvalidSettingsNegativeWidthTest()
        {
            string json = @"{
                              'board': {
                                'width': -6,
                                'height': 6,
                                    'mines':[
                                    { 'x':5, 'y':5},
                                    { 'x':3, 'y':2},
                                     { 'x':1, 'y':4}
                                    ]


                              },
                                'startTile':{'x':0,'y':0},
                                'exitTile':{'x':6, 'y':5},
                                'direction':'North'
                            }";

            var result = DeserializeAndValidate(json);

            Assert.False(result.IsValid);
            Assert.Equal("Board.Width", result.Errors[0].PropertyName);
            Assert.Equal("GreaterThanOrEqualValidator", result.Errors[0].ErrorCode);
        }

        [Fact]
        public void InvalidMineNegativeXTest()
        {
            string json = @"{
                              'board': {
                                'width': 6,
                                'height': 6,
                                    'mines':[
                                    { 'x':-5, 'y':5},
                                    { 'x':3, 'y':2},
                                     { 'x':1, 'y':4}
                                    ]


                              },
                                'startTile':{'x':0,'y':0},
                                'exitTile':{'x':4, 'y':5},
                                'direction':'North'
                            }";

            var result = DeserializeAndValidate(json);

            Assert.False(result.IsValid);
        }

        [Fact]
        public void InvalidMineNegativeYTest()
        {
            string json = @"{
                              'board': {
                                'width': 6,
                                'height': 6,
                                    'mines':[
                                    { 'x':5, 'y':-5},
                                    { 'x':3, 'y':2},
                                     { 'x':1, 'y':4}
                                    ]


                              },
                                'startTile':{'x':0,'y':0},
                                'exitTile':{'x':4, 'y':5},
                                'direction':'North'
                            }";

            var result = DeserializeAndValidate(json);

            Assert.False(result.IsValid);
        }

        [Fact]
        public void InvalidSettingsNegativeHeightTest()
        {
            string json = @"{
                              'board': {
                                'width': 6,
                                'height': -5,
                                 'mines':[
                                    { 'x':5, 'y':5},
                                    { 'x':3, 'y':2},
                                     { 'x':1, 'y':4}
                                    ]
                              },
                                'startTile':{'x':0,'y':0},
                                'exitTile':{'x':6, 'y':5},
                                'direction':'North'
                            }";
            var result = DeserializeAndValidate(json);

            Assert.False(result.IsValid);
        }


        [Fact]
        public void InvalidSettingsNoMinesTest()
        {
            string json = @"{
                              'board': {
                                'width': 7,
                                'height': 6
                              },
                                'startTile':{'x':0,'y':0},
                                'exitTile':{'x':6, 'y':5},
                                'direction':'North'
                            }";
            Settings settings = JsonConvert.DeserializeObject<Settings>(json);
            var settingsValidator = new SettingsValidator();
            Assert.False(settingsValidator.Validate(settings).IsValid);
        }


        [Fact]
        public void InvalidSettingsEmptyMinesTest()
        {
            string json = @"{
                              'board': {
                                'width': 6,
                                'height': 5,
                                'mines':[      
                                    
                                ]
                              },
                                'startTile':{'x':0,'y':0},
                                'exitTile':{'x':6, 'y':5},
                                'direction':'North'
                            }";
            Settings settings = JsonConvert.DeserializeObject<Settings>(json);
            var settingsValidator = new SettingsValidator();
            Assert.False(settingsValidator.Validate(settings).IsValid);
        }

        [Fact]
        public void InvalidSettingsInvalidMineXTest()
        {
            string json = @"{
                              'board': {
                                'width': 7,
                                'height': 6,
                                'mines':[      
                                     { 'x':7, 'y':5}
                                ]
                              },
                                'startTile':{'x':0,'y':0},
                                'exitTile':{'x':6, 'y':5},
                                'direction':'North'
                            }";
            Settings settings = JsonConvert.DeserializeObject<Settings>(json);
            var settingsValidator = new SettingsValidator();
            Assert.False(settingsValidator.Validate(settings).IsValid);
        }

        [Fact]
        public void InvalidSettingsInvalidMineYTest()
        {
            string json = @"{
                              'board': {
                                'width': 6,
                                'height': 5,
                                'mines':[      
                                     { 'x':6, 'y':6}
                                ]
                              },
                                'startTile':{'x':0,'y':0},
                                'exitTile':{'x':3, 'y':3},
                                'direction':'North'
                            }";
            Settings settings = JsonConvert.DeserializeObject<Settings>(json);
            var settingsValidator = new SettingsValidator();
            Assert.False(settingsValidator.Validate(settings).IsValid);
        }

        [Fact]
        public void InvalidSettingsInvalidStartXRangeTest()
        {
            string json = @"{
                              'board': {
                                'width': 6,
                                'height': 5,
                                'mines':[      
                                     { 'x':6, 'y':6}
                                ]
                              },
                                'startTile':{'x':7,'y':0},
                                'exitTile':{'x':6, 'y':5},
                                'direction':'North'
                            }";
            Settings settings = JsonConvert.DeserializeObject<Settings>(json);
            var settingsValidator = new SettingsValidator();
            Assert.False(settingsValidator.Validate(settings).IsValid);
        }

        [Fact]
        public void InvalidSettingsInvalidStartXNegativeTest()
        {
            string json = @"{
                              'board': {
                                'width': 6,
                                'height': 5,
                                'mines':[      
                                     { 'x':6, 'y':6}
                                ]
                              },
                                'startTile':{'x':'-1','y':0},
                                'exitTile':{'x':6, 'y':5},
                                'direction':'North'
                            }";
            Settings settings = JsonConvert.DeserializeObject<Settings>(json);
            var settingsValidator = new SettingsValidator();
            Assert.False(settingsValidator.Validate(settings).IsValid);
        }

        [Fact]
        public void InvalidSettingsInvalidStartYRangeTest()
        {
            string json = @"{
                              'board': {
                                'width': 6,
                                'height': 5,
                                'mines':[      
                                     { 'x':5, 'y':4}
                                ]
                              },
                                'startTile':{'x':5,'y':5},
                                'exitTile':{'x':5, 'y':3},
                                'direction':'North'
                            }";
            Settings settings = JsonConvert.DeserializeObject<Settings>(json);
            var settingsValidator = new SettingsValidator();
            Assert.False(settingsValidator.Validate(settings).IsValid);
        }

        [Fact]
        public void InvalidSettingsInvalidStartYNegativeTest()
        {
            string json = @"{
                              'board': {
                                'width': 6,
                                'height': 5,
                                'mines':[      
                                     { 'x':6, 'y':6}
                                ]
                              },
                                'startTile':{'x':6,'y':'-1'},
                                'exitTile':{'x':6, 'y':5},
                                'direction':'North'
                            }";
            Settings settings = JsonConvert.DeserializeObject<Settings>(json);
            var settingsValidator = new SettingsValidator();
            Assert.False(settingsValidator.Validate(settings).IsValid);
        }

        [Fact]
        public void MinesCantOverlapStartTileTest()
        {
            string json = @"{
                              'board': {
                                'width': 6,
                                'height': 6,
                                'mines':[
                                    { 'x':5, 'y':5},
                                    { 'x':3, 'y':2},
                                     { 'x':1, 'y':4}
                                    ]
                              },
                                'startTile':{'x':5,'y':5},
                                'exitTile':{'x':5, 'y':4},
                                'direction':'North'
                            }";

            var result = DeserializeAndValidate(json);

            Assert.False(result.IsValid);
        }

        private ValidationResult DeserializeAndValidate(string json)
        {
            Settings settings = JsonConvert.DeserializeObject<Settings>(json);
            var settingsValidator = new SettingsValidator();
            return settingsValidator.Validate(settings);

        }
    }
}
